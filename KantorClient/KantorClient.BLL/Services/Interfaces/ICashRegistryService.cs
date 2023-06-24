using KantorClient.BLL.Models;
using KantorClient.Model;

namespace KantorClient.BLL.Services.Interfaces
{
    public interface ICashRegistryService
    {
        Task<List<CashRegistryModel>> GetRegistries();
        Task<CashRegistryModel> AddRegistry(CashRegistryModel registry);
        Task<CashRegistryModel> EditRegistry(CashRegistryModel registry);
        Task<bool> AddCurrency(Currency currency, decimal value);
        Task<bool> DeleteRegistry(CashRegistryModel registry);
        Task<decimal> GetAmountForCurrency(CurrencyModel currency);
    }
}
