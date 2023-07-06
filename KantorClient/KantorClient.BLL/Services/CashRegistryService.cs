using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;

namespace KantorClient.BLL.Services
{
    public class CashRegistryService : ICashRegistryService
    {
        private readonly ICashRegistryRepository _cashRegistryRepository;

        public CashRegistryService(ICashRegistryRepository cashRegistryRepository)
        {
            _cashRegistryRepository = cashRegistryRepository;
        }

        public async Task<bool> AddCurrency(Currency currency, decimal value)
        {
            var registry = await GetRegistryForCurrency(currency);
            if (registry != null)
            {
                registry.Quantity += value;
                await EditRegistry(registry);
                return true;
            }
            return false;
        }

        public async Task<CashRegistryModel> AddRegistry(CashRegistryModel registry)
        {
            try
            {
                var entity = registry.Map();
                var addedEntity = await _cashRegistryRepository.AddRegistry(entity);
                return new CashRegistryModel(addedEntity);
            }
            catch { }
            return null;
        }

        public async Task<bool> DeleteRegistry(CashRegistryModel registry)
        {
            try
            {
                var entity = registry.Map();
                var deletedEntity = await _cashRegistryRepository.DeleteRegistry(entity);
                return deletedEntity != null;
            }
            catch { }
            return false;
        }

        public async Task<CashRegistryModel> EditRegistry(CashRegistryModel registry)
        {
            try
            {
                var entity = registry.Map();
                var editedEntity = await _cashRegistryRepository.EditRegistry(entity);
                if (editedEntity == null)
                    return null;
                return new CashRegistryModel(editedEntity);
            }
            catch { }
            return null;
        }

        public async Task<decimal> GetAmountForCurrency(CurrencyModel currency)
        {
            try
            {
                var entity = currency.Map();
                var registry = await _cashRegistryRepository.GetRegistryForCurrency(entity);
                return registry?.Quantity ?? 0;
            }
            catch { }
            return 0;
        }

        public async Task<List<CashRegistryModel>> GetRegistries()
        {
            try
            {
                var registries = await _cashRegistryRepository.GetLocalRegistries();
                if (registries == null)
                {
                    return new List<CashRegistryModel>();
                }

                var registryModels = registries.Select(x => new CashRegistryModel(x));
                return registryModels.ToList();
            }
            catch (Exception ex)
            {
                //TODO: logging
            }
            return new List<CashRegistryModel>();
        }

        private async Task<CashRegistryModel> GetRegistryForCurrency(Currency currency)
        {
            try
            {
                var registry = await _cashRegistryRepository.GetRegistryForCurrency(currency);
                if (registry == null)
                {
                    return null;
                }

                return new CashRegistryModel(registry);
            }
            catch(Exception ex) 
            {

            }

            return null;
        }
    }
}
