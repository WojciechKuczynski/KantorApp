using KantorClient.BLL.Models;

namespace KantorClient.BLL.Services.Interfaces
{
    public interface ICashRegistryService
    {
        Task<List<CashRegistryModel>> GetRegistries();
        Task<CashRegistryModel> AddRegistry(CashRegistryModel registry);
        Task<CashRegistryModel> EditRegistry(CashRegistryModel registry);
        Task<bool> DeleteRegistry(CashRegistryModel registry);
    }
}
