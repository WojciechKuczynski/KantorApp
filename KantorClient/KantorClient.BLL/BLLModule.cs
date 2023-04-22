using KantorClient.BLL.Services;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.Common;
using KantorClient.DAL.Repositories.Interfaces;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.BLL
{
    public class BLLModule : IModule
    {
        public void SetDependencies(Container container)
        {
            container.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            container.RegisterSingleton<ISettingsService, SettingsService>();
        }
    }
}
