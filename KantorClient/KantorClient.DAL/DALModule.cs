using KantorClient.Common;
using KantorClient.DAL.Repositories;
using KantorClient.DAL.Repositories.Interfaces;
using SimpleInjector;

namespace KantorClient.DAL
{
    public class DALModule : IModule
    {
        public void SetDependencies(Container container)
        {
            container.RegisterSingleton<DataContext>();
            container.RegisterSingleton<IUserRepository, UserRepository>();
            container.RegisterSingleton<ISettingsRepository, SettingsRepository>();
            container.RegisterSingleton<ISynchronizationRepository, SynchronizationRepository>();
            container.RegisterSingleton<ITransactionsRepository, TransactionsRepository>();
            container.RegisterSingleton<ITransferRepository, TransferRepository>();
            container.RegisterSingleton<ICashRegistryRepository, CashRegistryRepository>();

            container.RegisterSingleton<IConfigurationRepository, ConfigurationRepository>();
            container.RegisterSingleton<IReportsRepository, ReportsRepository>();
        }
    }
}
