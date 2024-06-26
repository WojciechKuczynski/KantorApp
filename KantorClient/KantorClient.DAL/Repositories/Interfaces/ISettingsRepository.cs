﻿using KantorClient.Model;

namespace KantorClient.DAL.Repositories.Interfaces
{
    public interface ISettingsRepository
    {
        public Task<List<Currency>> GetCurrencies(string synchronizationKey);
        public Task<List<Rate>> GetRates(string synchronizationKey);
        public Task<List<Rate>> AddRates(IEnumerable<Rate> rates);
        public Task<bool> AddCurrencies(IEnumerable<Currency> currencies);
        public Task<Rate> AddNewRate(Rate rate);
        public Task<Rate> EditRate(Rate rate);
        public Task<bool> RemoveRate(Rate rate);

        public Task<List<Rate>> GetNBPRates();
    }
}
