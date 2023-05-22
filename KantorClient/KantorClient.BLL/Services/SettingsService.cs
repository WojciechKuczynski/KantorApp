using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;

namespace KantorClient.BLL.Services
{
    internal class SettingsService : ISettingsService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsRepository _settingsRepository;

        public SettingsService(IAuthenticationService authenticationService, ISettingsRepository settingsRepository)
        {
            _authenticationService = authenticationService;
            _settingsRepository = settingsRepository;

        }

        public List<Currency> Currencies { get; private set; }

        public List<Rate> Rates { get; private set; }

        public async Task<Rate> AddRate(Rate rate)
        {
            return await _settingsRepository.AddNewRate(rate);
        }

        public async Task<Rate> EditRate(Rate rate)
        {
            return await _settingsRepository.EditRate(rate);
        }

        public async Task<bool> LoadSettings()
        {
            try
            {
                Currencies = await LoadCurrencies();
                await _settingsRepository.AddCurrencies(Currencies);
                var newRates = await LoadRates();
                Rates = await _settingsRepository.AddRates(newRates); // Co z tym?

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<List<Currency>> LoadCurrencies()
        {
            var currencyList = await _settingsRepository.GetCurrencies(_authenticationService.UserSession.SynchronizationKey);

            return currencyList;
        }

        private async Task<List<Rate>> LoadRates()
        {
            var rates = await _settingsRepository.GetRates(_authenticationService.UserSession.SynchronizationKey);
            return rates;
        }
    }
}
