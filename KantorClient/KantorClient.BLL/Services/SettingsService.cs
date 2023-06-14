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
        public List<Rate> NbpRates { get; set; }

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
            var nbpRates = await _settingsRepository.GetNBPRates();
            foreach (var rate in nbpRates)
            {
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
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveRate(Rate rate)
        {
            return await _settingsRepository.RemoveRate(rate);
        }


        private async Task<List<Currency>> LoadCurrencies()
        {
            var currencyList = await _settingsRepository.GetCurrencies(_authenticationService.UserSession.SynchronizationKey);

            return currencyList;
        }

        public async Task LoadRates()
        {
            var rates = await _settingsRepository.GetRates(_authenticationService.UserSession.SynchronizationKey);
            Rates = await _settingsRepository.AddRates(rates); // Co z tym?
        }
    }
}
