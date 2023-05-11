using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.DAL.ServerCommunication;
using KantorClient.Model;
using KantorServer.Application.Requests.Currencies;
using KantorServer.Application.Requests.Rates;
using KantorServer.Application.Responses;
using KantorServer.Application.Responses.Currencies;
using Microsoft.EntityFrameworkCore;

namespace KantorClient.DAL.Repositories
{
    internal class SettingsRepository : ISettingsRepository
    {
        public SettingsRepository()
        {
        }

        public async Task<bool> AddCurrencies(IEnumerable<Currency> currencies)
        {
            using (var context = new DataContext())
            {
                var currenciesToExclude = await context.Currencies.Where(x => currencies.Select(y => y.ExternalId.ToString()).Contains(x.ExternalId)).ToListAsync();
                var currenciesToDb = currencies.Where(x => !currenciesToExclude.Any(y => y.ExternalId == x.ExternalId));
                await context.Currencies.AddRangeAsync(currenciesToDb);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Rate> AddNewRate(Rate rate)
        {
            using (var context = new DataContext())
            {
                rate.Currency = await context.Currencies.FindAsync(rate.Currency.Id);
                var addedRate = await context.Rates.AddAsync(rate);
                await context.SaveChangesAsync();
                return addedRate.Entity;
            }
        }

        public async Task<List<Rate>> AddRates(IEnumerable<Rate> rates)
        {
            using (var context = new DataContext())
            {
                var ratesIds = rates.Select(rates => rates.Id).ToList();
                var ratesToExclude = await context.Rates.Where(x => x.ExternalId.HasValue && ratesIds.Contains(x.ExternalId.Value)).ToListAsync();
                var ratesToDb = rates.Where(x => !ratesToExclude.Any(y => y.ExternalId == x.ExternalId));
                foreach (var rate in ratesToDb)
                {
                    var currency = await context.Currencies.FirstOrDefaultAsync(x => x.ExternalId == rate.Currency.ExternalId);
                    if (currency != null)
                    {
                        rate.Currency = currency;
                    }
                }
                await context.Rates.AddRangeAsync(ratesToDb);
                await context.SaveChangesAsync();
                return ratesToDb.ToList();
            }
        }

        public async Task<Rate> EditRate(Rate rate)
        {
            using (var context = new DataContext())
            {
                var editedRate = context.Rates.Update(rate);
                await context.SaveChangesAsync();
                return editedRate.Entity;
            }
        }

        public async Task<List<Currency>> GetCurrencies(string synchronizationKey)
        {
            var request = new GetAllCurrenciesRequest()
            {
                SynchronizationKey = synchronizationKey
            };
            var requestContext = new RequestContext("https://localhost:7254/currencies/all", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<GetAllCurrenciesRequest, GetAllCurrenciesResponse>(requestContext, request);

            return response.Currencies.Select(x => new Currency(x)).ToList();
        }

        public async Task<List<Rate>> GetRates(string synchronizationKey)
        {
            var request = new GetAllRatesRequest()
            {
                SynchronizationKey = synchronizationKey
            };
            var requestContext = new RequestContext("https://localhost:7254/rates/all", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<GetAllRatesRequest, GetAllRatesResponse>(requestContext, request);

            return response.Rates.Select(x => new Rate(x)).ToList();
        }
    }
}
