using KantorClient.BLL.Models;
using KantorClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task GetNBPRates();
    }
}
