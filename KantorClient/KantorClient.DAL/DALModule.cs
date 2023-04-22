using KantorClient.Common;
using KantorClient.DAL.Repositories;
using KantorClient.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
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
        }
    }
}
