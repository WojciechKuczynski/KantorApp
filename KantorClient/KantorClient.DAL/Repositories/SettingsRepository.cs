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
        private readonly DataContext _dataContext;
        public SettingsRepository(DataContext datacontext)
        {
            _dataContext = datacontext;
        }

        public async Task<bool> AddCurrencies(IEnumerable<Currency> currencies)
        {
            try
            {
                var currenciesToExclude = await _dataContext.Currencies.Where(x => currencies.Select(y => y.ExternalId.ToString()).Contains(x.ExternalId)).ToListAsync();
                var currenciesToDb = currencies.Where(x => !currenciesToExclude.Any(y => y.ExternalId == x.ExternalId));
                await _dataContext.Currencies.AddRangeAsync(currenciesToDb);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Rate>> AddRates(IEnumerable<Rate> rates)
        {
            try
            {
                var ratesIds = rates.Select(rates => rates.Id.ToString());
                var ratesToExclude = await _dataContext.Rates.Where(x => ratesIds.Contains(x.ExternalId)).ToListAsync();
                var ratesToDb = rates.Where(x => !ratesToExclude.Any(y => y.ExternalId == x.ExternalId));
                foreach (var rate in ratesToDb)
                {
                    var currency = await _dataContext.Currencies.FirstOrDefaultAsync(x => x.ExternalId == rate.Currency.ExternalId);
                    if (currency != null)
                    {
                        rate.Currency = currency;
                    }
                }
                await _dataContext.Rates.AddRangeAsync(ratesToDb);
                await _dataContext.SaveChangesAsync();
                return ratesToDb.ToList();
            }
            catch (Exception ex)
            {
                //TODO: dodać logowanie? albo komunikat?
                return null;
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

            // TODO: dorobić mapowanie
            return response.Rates.Select(x => new Rate(x)).ToList();
        }
    }
}
