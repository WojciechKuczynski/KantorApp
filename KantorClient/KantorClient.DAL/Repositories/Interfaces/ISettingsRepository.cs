using KantorClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.DAL.Repositories.Interfaces
{
    public interface ISettingsRepository
    {
        public Task<List<Currency>> GetCurrencies(string synchronizationKey);
        public Task<List<Rate>> GetRates(string synchronizationKey);
        public Task<List<Rate>> AddRates(IEnumerable<Rate> rates);
        public Task<bool> AddCurrencies(IEnumerable<Currency> currencies);
    }
}
