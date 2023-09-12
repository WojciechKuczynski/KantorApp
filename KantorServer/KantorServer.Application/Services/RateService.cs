using KantorServer.Application.Services.Interfaces;
using KantorServer.DAL;
using KantorServer.Model.Dtos;
using Microsoft.EntityFrameworkCore;

namespace KantorServer.Application.Services
{
    public class RateService : IRateService
    {
        public DataContext DataContext { get; }

        public RateService(DataContext dataContext)
        {
            DataContext = dataContext;
        }
        public async Task<RateDto> AddEditRate(RateDto rate)
        {
            try
            {
                var rateInDb = await DataContext.Rates.Include(x => x.Currency).FirstOrDefaultAsync(x => x.ExternalId == rate.ExternalId);
                if (rateInDb == null)
                {
                    var rateEntity = rate.ConvertToEntity();
                    var currencyInDb = await DataContext.Currencies.FirstOrDefaultAsync(x => x.Symbol == rate.Currency.Symbol);
                    if (currencyInDb != null)
                    {
                        rateEntity.Currency = currencyInDb;
                    }
                    await InvalidateRatesBySymbol(rateEntity.Currency.Symbol);
                    rateEntity.Valid = true;
                    await DataContext.Rates.AddAsync(rateEntity);
                    await DataContext.SaveChangesAsync();
                    return new RateDto(rateEntity);
                }
                else
                {
                    if (rateInDb.StartDate >= DateTime.Now)
                    {
                        // cannot change if already started
                        return null;
                    }

                    if (rate.Valid)
                    {
                        await InvalidateRatesBySymbol(rateInDb.Currency.Symbol);
                    }

                    rateInDb.StartDate = rate.StartDate;
                    rateInDb.EndDate = rate.EndDate;
                    rateInDb.MaximumBuyRate = rate.MaximumBuyRate;
                    rateInDb.DefaultBuyRate = rate.DefaultBuyRate;
                    rateInDb.MinimalSellRate = rate.MinimalSellRate;
                    rateInDb.DefaultSellRate = rate.DefaultSellRate;
                    rateInDb.Valid = rate.Valid;
                }
                await DataContext.SaveChangesAsync();
                return new RateDto(rateInDb);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> RemoveRate(RateDto rate)
        {
            try
            {
                var rateInDb = await DataContext.Rates.FirstOrDefaultAsync(x => x.Id == rate.Id);
                if (rateInDb == null)
                {
                    return false;
                }

                rateInDb.Valid = false;
                await DataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Model.Rate>> GetAllRates()
        {
            try
            {
                var rates = await DataContext.Rates.Include(x => x.Currency).Where(x => x.Valid).ToListAsync();

                return rates;
            }
            catch (Exception ex) { return null; }
        }

        private async Task<bool> InvalidateRatesBySymbol(string symbol)
        {
            try
            {
                var ratesInDb = await DataContext.Rates.Where(x => x.Currency.Symbol == symbol).ToArrayAsync();
                if (ratesInDb == null)
                {
                    return false;
                }

                foreach (var rate in ratesInDb)
                {
                    rate.Valid = false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
