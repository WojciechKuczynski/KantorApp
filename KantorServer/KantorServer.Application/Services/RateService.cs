using KantorServer.Application.Services.Interfaces;
using KantorServer.DAL;
using KantorServer.Model;
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
        public async Task<bool> AddEditRate(RateDto rate)
        {
            try
            {
                var rateInDb = await DataContext.Rates.FirstOrDefaultAsync(x => x.Id == rate.Id);
                if (rateInDb == null)
                {
                    var rateEntity = rate.ConvertToEntity();
                    await DataContext.Rates.AddAsync(rateEntity);
                }
                else
                {
                    if (rateInDb.StartDate >= DateTime.Now)
                    {
                        // cannot change if already started
                        return false;
                    }

                    rateInDb.StartDate = rate.StartDate;
                    rateInDb.EndDate = rate.EndDate;
                    rateInDb.MinimalRate = rate.MinimalRate;
                    rateInDb.DefaultRate = rate.DefaultRate;
                }
                await DataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
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

        public async Task<List<Rate>> GetAllRates()
        {
            try
            {
                var rates = await DataContext.Rates.Where(x => x.Valid).ToListAsync();

                return rates;
            }
            catch (Exception ex) { return null; }
        }
    }
}
