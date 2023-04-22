using KantorClient.Application.ViewModels;
using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Rates;
using KantorClient.Application.ViewModels.Rates;
using KantorClient.Common;
using SimpleInjector;

namespace KantorClient.Application
{
    public class ApplicationModule : IModule
    {
        public void SetDependencies(Container container)
        {
            container.RegisterSingleton<IMainWindowViewModel, MainWindowViewModel>();
            container.RegisterSingleton<IRatesMainViewModel, RatesMainViewModel>();
        }
    }
}
