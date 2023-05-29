using KantorClient.Application.Consts;
using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Rates;
using KantorClient.BLL.Services.Interfaces;
using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using KantorClient.Application.ViewModels.Interfaces.Transactions;
using KantorClient.Application.ViewModels.Interfaces.Users;
using KantorClient.Application.ViewModels.Interfaces.Transfers;

namespace KantorClient.Application.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged, IMainWindowViewModel
    {
        private readonly ISettingsService _settingService;

        public event PropertyChangedEventHandler? PropertyChanged;

        #region MainViews
        public MainWindowView FormType { get; private set; }

        public IRatesMainViewModel RatesMainVM { get; private set; }

        public ITransactionsMainViewModel TransactionsMainVM { get; private set; } 

        public IUsersMainViewModel UsersMainVM { get; private set; }

        public ITransfersMainViewModel TransfersMainVM { get; private set; }
        #endregion

        public MainWindowViewModel(ISettingsService settingsService, IRatesMainViewModel ratesMainVM, 
                                   ITransactionsMainViewModel transactionsMainVM, IUsersMainViewModel usersMainViewModel, 
                                   ITransfersMainViewModel transfersMainViewModel)
        {
            _settingService = settingsService;

            RatesMainVM = ratesMainVM;
            RatesMainVM.Parent = this;

            TransactionsMainVM = transactionsMainVM;
            TransactionsMainVM.Parent = this;

            UsersMainVM = usersMainViewModel;
            UsersMainVM.Parent = this;

            TransfersMainVM = transfersMainViewModel;
            TransfersMainVM.Parent = this;

            RatesMainViewCommand = new DelegateCommand(RatesMainView);
            TransactionsMainViewCommand = new DelegateCommand(TransactionsMainView);
            UsersMainViewCommand = new DelegateCommand(UsersMainView);
            TransfersMainViewCommand = new DelegateCommand(TransfersMainView);
        }

        public Window Parent { get; set; }

        public bool LoggedOut { get; set; }

        public async Task Load()
        {
            var loaded = await _settingService.LoadSettings();
            await RatesMainVM.Load(loaded);
            await TransactionsMainVM.Load(loaded);
            await UsersMainVM.Load(loaded);
            await TransfersMainVM.Load(loaded);
        }

        #region Commands
        public ICommand RatesMainViewCommand { get; private set; }
        private void RatesMainView()
        {
            FormType = MainWindowView.Rates;
        }

        public ICommand TransactionsMainViewCommand { get; private set; }
        private void TransactionsMainView()
        {
            FormType = MainWindowView.TransactionList;
        }

        public ICommand UsersMainViewCommand { get; private set; }
        private void UsersMainView()
        {
            FormType = MainWindowView.Users;
        }

        public ICommand TransfersMainViewCommand { get; private set; }
        private void TransfersMainView()
        {
            FormType = MainWindowView.Transfers;
        }
        #endregion
    }
}
