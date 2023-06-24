using KantorClient.Model;

namespace KantorClient.DAL.Repositories.Interfaces
{
    public interface ICashRegistryRepository
    {
        Task<IEnumerable<CashRegistry>> GetLocalRegistries();
        Task<CashRegistry> AddRegistry(CashRegistry registry);
        Task<CashRegistry> DeleteRegistry(CashRegistry registry);
        Task<CashRegistry> EditRegistry(CashRegistry registry);
        Task<CashRegistry> GetRegistryForCurrency(Currency currency);
    }
}
