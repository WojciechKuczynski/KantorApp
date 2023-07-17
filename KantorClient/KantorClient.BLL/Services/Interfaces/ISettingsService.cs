using KantorClient.Common.Events;
using KantorClient.Model;

namespace KantorClient.BLL.Services.Interfaces
{
    public interface ISettingsService
    {
        public List<Rate> NbpRates { get; set; }

        public Task<bool> LoadSettings();
        public List<Currency> Currencies { get; }
        public List<Rate> Rates { get; }
        public Task<Rate> AddRate(Rate rate);
        public Task<Rate> EditRate(Rate rate);
        public Task<bool> RemoveRate(Rate rate);
        public Task LoadRates();

        public Task GetNBPRates();

        public event DataUpdated DataUpdated;
    }
}
