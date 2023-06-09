using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;

namespace KantorClient.DAL.Repositories
{
    public class CashRegistryRepository : ICashRegistryRepository
    {
        public Task<CashRegistry> AddRegistry(CashRegistry registry)
        {
            throw new NotImplementedException();
        }

        public Task<CashRegistry> DeleteRegistry(CashRegistry registry)
        {
            throw new NotImplementedException();
        }

        public Task<CashRegistry> EditRegistry(CashRegistry registry)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CashRegistry>> GetLocalRegistries()
        {
            throw new NotImplementedException();
        }
    }
}
