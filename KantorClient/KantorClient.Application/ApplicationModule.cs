using KantorClient.Application.ViewModels;
using KantorClient.Application.ViewModels.CashRegistry;
using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.CashRegistry;
using KantorClient.Application.ViewModels.Interfaces.Rates;
using KantorClient.Application.ViewModels.Interfaces.Reports;
using KantorClient.Application.ViewModels.Interfaces.Transactions;
using KantorClient.Application.ViewModels.Interfaces.Transfers;
using KantorClient.Application.ViewModels.Interfaces.Users;
using KantorClient.Application.ViewModels.Rates;
using KantorClient.Application.ViewModels.Reports;
using KantorClient.Application.ViewModels.Transactions;
using KantorClient.Application.ViewModels.Transfers;
using KantorClient.Application.ViewModels.Users;
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
            container.RegisterSingleton<IRatesAddEditViewModel, RatesAddEditViewModel>();

            container.RegisterSingleton<ITransactionsMainViewModel, TransactionsMainViewModel>();
            container.RegisterSingleton<ITransactionsAddEditViewModel, TransactionsAddEditViewModel>();

            container.RegisterSingleton<IUsersMainViewModel, UsersMainViewModel>();

            container.RegisterSingleton<ITransfersMainViewModel, TransfersMainViewModel>();

            container.RegisterSingleton<ICashRegistryMainViewModel, CashRegistryMainViewModel>();

            container.RegisterSingleton<IReportsMainViewModel, ReportsMainViewModel>();

        }
    }
}
