using KantorClient.Common.Exceptions;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.DAL.RequestArgs;
using KantorClient.DAL.ResponseArgs;
using KantorClient.DAL.ServerCommunication;
using KantorClient.Model;
using KantorServer.Application.Requests.Currencies;
using KantorServer.Application.Requests.Rates;
using KantorServer.Application.Responses.Currencies;
using KantorServer.Application.Responses.Rates;
using Microsoft.EntityFrameworkCore;

namespace KantorClient.DAL.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly IConfigurationRepository _configurationRepository;
        public SettingsRepository(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
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
                rate.Synchronized = false;
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
                var ratesIds = rates.Select(rates => rates.ExternalId).ToList();
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
                await InvalidateRates(context, rates);
                await context.Rates.AddRangeAsync(ratesToDb);
                await context.SaveChangesAsync();
                var res = await context.Rates
                    .Include(x => x.Currency)
                    .Where(x => x.Valid).ToListAsync();
                return res;
            }
        }

        private async Task InvalidateRates(DataContext context, IEnumerable<Rate> rates)
        {
            var idWithRate = rates.ToDictionary(x => x.ExternalId);
            var ratesSynchronized = await context.Rates.Where(x => x.ExternalId != null).ToListAsync();
            var ratesToInvalidate = ratesSynchronized.Where(x => idWithRate.ContainsKey(x.ExternalId));
            foreach (var rate in ratesSynchronized) 
            {
                if (idWithRate.ContainsKey(rate.ExternalId))
                {
                    rate.Valid = idWithRate[rate.ExternalId.Value].Valid;
                }
                else if(rate.EndDate < DateTime.Now)
                {
                    rate.Valid = false;
                }
                else
                {
                    rate.Valid = true;
                }
            }
        }

        public async Task<Rate> EditRate(Rate rate)
        {
            using (var context = new DataContext())
            {
                rate.Synchronized = false;
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
            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/currencies/all", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<GetAllCurrenciesRequest, GetAllCurrenciesResponse>(requestContext, request);
            if (response == null)
            {
                throw new ServerNotReachedException();
            }

            return response.Currencies.Select(x => new Currency(x)).ToList();
        }

        public async Task<List<Rate>> GetNBPRates()
        {
            try
            {
                var requestContext = new RequestContext("http://api.nbp.pl/api/exchangerates/tables/A/last", RestSharp.Method.Get);
                var response = await ServerConnectionHandler.ExecuteFunction<NbpRequestArgs, NbpResponseArgs>(requestContext, null);

                return response.Root[0].rates.Select(x => new Rate()
                {
                    Synchronized = true,
                    Currency = new Currency { Symbol = x.code, Name = x.currency },
                    DefaultBuyRate = x.mid
                }).ToList();
            }
            catch (Exception)
            {
                return new List<Rate>
                {
                    new Rate() {Synchronized = true, Currency = new Currency{Symbol = "USD", Name="dolar amerykański"}, DefaultBuyRate = 4.3077M},
                    new Rate() {Synchronized = true, Currency = new Currency{Symbol = "EUR", Name="Euro"}, DefaultBuyRate = 4.6209M},
                    new Rate() {Synchronized = true, Currency = new Currency{Symbol = "GBP", Name="Funt szterling"}, DefaultBuyRate = 5.3926M},
                };
            }
        }

        public async Task<List<Rate>> GetRates(string synchronizationKey)
        {
            var request = new GetAllRatesRequest()
            {
                SynchronizationKey = synchronizationKey
            };

            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/rates/all", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<GetAllRatesRequest, GetAllRatesResponse>(requestContext, request);
            if (response?.Rates == null)
            {
                throw new ServerNotReachedException();
            }
            return response.Rates.Select(x => new Rate(x) { Synchronized = true }).ToList();
        }

        public async Task<bool> RemoveRate(Rate rate)
        {
            using (var context = new DataContext())
            {
                rate.Valid = false;
                rate.Synchronized = false;
                var editedRate = context.Rates.Update(rate);
                await context.SaveChangesAsync();
                return editedRate != null;
            }
        }
    }
}
