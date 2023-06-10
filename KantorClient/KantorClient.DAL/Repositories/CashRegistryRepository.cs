using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;
using KantorServer.Model;
using Microsoft.EntityFrameworkCore;

namespace KantorClient.DAL.Repositories
{
    public class CashRegistryRepository : ICashRegistryRepository
    {
        public async Task<CashRegistry> AddRegistry(CashRegistry registry)
        {
            using var dataContext = new DataContext();
            registry.Currency = await dataContext.Currencies.FindAsync(registry.Currency.Id);
            if (registry.Currency == null)
            {
                return null;
            }

            // If someone deleted Registry ( set quantity to 0 ), we get this existing entity
            var registryInDb = await dataContext.CashRegistries.Include(x => x.Currency).FirstOrDefaultAsync(x => x.Currency.Symbol == registry.Currency.Symbol);
            if (registryInDb != null)
            {
                registryInDb.LastUpdate = DateTime.Now;
                await dataContext.SaveChangesAsync();
                return registryInDb;
            }

            registry.LastUpdate = DateTime.Now;
            await dataContext.CashRegistries.AddAsync(registry);
            await dataContext.SaveChangesAsync();
            return registry;
        }

        public async Task<CashRegistry> DeleteRegistry(CashRegistry registry)
        {
            using var dataContext = new DataContext();
            var registryInDb = await dataContext.CashRegistries.FindAsync(registry.Id);
            if (registryInDb == null)
            {
                return null;
            }
            registryInDb.Quantity = 0;
            registryInDb.LastUpdate = DateTime.Now;
            await dataContext.SaveChangesAsync();
            return registryInDb;
        }

        public async Task<CashRegistry> EditRegistry(CashRegistry registry)
        {
            using var dataContext = new DataContext();
            var registryInDb = await dataContext.CashRegistries.FindAsync(registry.Id);
            if (registryInDb == null)
            {
                return null;
            }

            registryInDb.LastUpdate = DateTime.Now;
            registryInDb.Quantity = registry.Quantity;
            await dataContext.SaveChangesAsync();
            return registryInDb;
        }

        public async Task<IEnumerable<CashRegistry>> GetLocalRegistries()
        {
            using var dataContext = new DataContext();
            var localRegistries = await dataContext.CashRegistries.Include(x => x.Currency).Take(100).ToListAsync();

            return localRegistries;
        }
    }
}
