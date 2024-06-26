﻿using KantorClient.BLL.Services.Interfaces;
using KantorClient.Common.Events;
using KantorClient.Common.Exceptions;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;

namespace KantorClient.BLL.Services
{
    internal class SettingsService : ISettingsService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsRepository _settingsRepository;
        private object _rateSynchroLock = new object();
        private SemaphoreSlim _rateSymaphore = new SemaphoreSlim(1);

        public event DataUpdated DataUpdated;

        public SettingsService(IAuthenticationService authenticationService, ISettingsRepository settingsRepository)
        {
            _authenticationService = authenticationService;
            _settingsRepository = settingsRepository;

            _authenticationService.OnlineModeChanged += _authenticationService_OnlineModeChanged;
        }

        private void _authenticationService_OnlineModeChanged(object sender, bool newValue)
        {
            OnlineMode = newValue;
        }

        public List<Currency> Currencies { get; private set; }

        public List<Rate> Rates { get; private set; }
        public List<Rate> NbpRates { get; set; }
        public bool OnlineMode { get; set; }

        public async Task<Rate> AddRate(Rate rate)
        {
            return await _settingsRepository.AddNewRate(rate);
        }

        public async Task<Rate> EditRate(Rate rate)
        {
            return await _settingsRepository.EditRate(rate);
        }

        public async Task GetNBPRates()
        {
            NbpRates = await _settingsRepository.GetNBPRates();
            Console.WriteLine("GetNbpRates");
            foreach (var rate in NbpRates)
            {
                Console.WriteLine("GetNbpRates + rate = " + rate.Currency.Name);
                var curr = Currencies.FirstOrDefault(x => x.Symbol == rate.Currency.Symbol);
                if (curr != null)
                {
                    rate.Currency = curr;
                }
            }
        }

        public async Task<bool> LoadSettings()
        {
            try
            {
                Currencies = await LoadCurrencies();
                await _settingsRepository.AddCurrencies(Currencies);
                await LoadRates();
                await GetNBPRates();
                return true;
            }
            catch (ServerNotReachedException)
            {
                _authenticationService.SetOnlineMode(false);
            }

            return false;
        }

        public async Task<bool> RemoveRate(Rate rate)
        {
            return await _settingsRepository.RemoveRate(rate);
        }


        private async Task<List<Currency>> LoadCurrencies()
        {
            try
            {
                var currencyList = await _settingsRepository.GetCurrencies(_authenticationService.UserSession.SynchronizationKey);

                return currencyList;
            }
            catch (ServerNotReachedException)
            {
                _authenticationService.SetOnlineMode(false);
            }
            return new List<Currency>();
        }

        public async Task LoadRates()
        {
            await _rateSymaphore.WaitAsync();
            try
            {
                if (!OnlineMode)
                {
                    return;
                }

                var rates = await _settingsRepository.GetRates(_authenticationService.UserSession.SynchronizationKey);
                Rates = await _settingsRepository.AddRates(rates); // Co z tym?
                DataUpdated?.Invoke(this, DateTime.Now);
            }
            catch (ServerNotReachedException)
            {
                _authenticationService.SetOnlineMode(false);
            }
            finally
            {
                _rateSymaphore.Release();
            }
        }
    }
}
