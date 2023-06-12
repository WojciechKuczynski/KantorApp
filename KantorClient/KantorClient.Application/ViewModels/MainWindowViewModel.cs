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
using KantorClient.Application.ViewModels.Interfaces.CashRegistry;
using KantorClient.Model;

namespace KantorClient.Application.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged, IMainWindowViewModel
    {
        private readonly ISettingsService _settingService;
        private readonly IAuthenticationService _authenticationService;

        public event PropertyChangedEventHandler? PropertyChanged;

        #region MainViews
        public MainWindowView FormType { get; private set; }

        public IRatesMainViewModel RatesMainVM { get; private set; }

        public ITransactionsMainViewModel TransactionsMainVM { get; private set; } 

        public IUsersMainViewModel UsersMainVM { get; private set; }

        public ITransfersMainViewModel TransfersMainVM { get; private set; }

        public ICashRegistryMainViewModel CashRegistryMainVM { get; private set; }
        #endregion

        public MainWindowViewModel(ISettingsService settingsService,IAuthenticationService authenticationService, IRatesMainViewModel ratesMainVM, 
                                   ITransactionsMainViewModel transactionsMainVM, IUsersMainViewModel usersMainViewModel, 
                                   ITransfersMainViewModel transfersMainViewModel, ICashRegistryMainViewModel cashRegistryMainViewModel)
        {
            _settingService = settingsService;
            _authenticationService = authenticationService;

            RatesMainVM = ratesMainVM;
            RatesMainVM.Parent = this;

            TransactionsMainVM = transactionsMainVM;
            TransactionsMainVM.Parent = this;

            UsersMainVM = usersMainViewModel;
            UsersMainVM.Parent = this;

            TransfersMainVM = transfersMainViewModel;
            TransfersMainVM.Parent = this;

            CashRegistryMainVM = cashRegistryMainViewModel;
            CashRegistryMainVM.Parent = this;

            RatesMainViewCommand = new DelegateCommand(RatesMainView);
            TransactionsMainViewCommand = new DelegateCommand(TransactionsMainView);
            UsersMainViewCommand = new DelegateCommand(UsersMainView);
            TransfersMainViewCommand = new DelegateCommand(TransfersMainView);
            CashRegistryMainViewCommand = new DelegateCommand(CashRegistryMainView);

            _authenticationService.CashUpdated += _authenticationService_CashUpdated;
        }

        private void _authenticationService_CashUpdated(object sender, decimal newValue)
        {
            Cash = newValue;
        }

        public Window Parent { get; set; }

        public bool LoggedOut { get; set; }

        public decimal Cash { get; set; }

        public UserSession Session { get; set; }

        public async Task Load()
        {
            var loaded = await _settingService.LoadSettings();

            Session = _authenticationService.UserSession;
            Cash = Session.Cash;
            await RatesMainVM.Load(loaded);
            await TransactionsMainVM.Load(loaded);
            await UsersMainVM.Load(loaded);
            await TransfersMainVM.Load(loaded);
            await CashRegistryMainVM.Load(loaded);
        }

        public void SetPln(decimal quantity)
        {
            Cash = quantity;
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

        public ICommand CashRegistryMainViewCommand { get; private set; }
        private void CashRegistryMainView()
        {
            FormType = MainWindowView.CashRegistry;
        }
        #endregion
    }
}
