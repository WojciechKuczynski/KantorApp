using KantorClient.Application.Consts;
using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.CashRegistry;
using KantorClient.Application.ViewModels.Interfaces.Rates;
using KantorClient.Application.ViewModels.Interfaces.Reports;
using KantorClient.Application.ViewModels.Interfaces.Transactions;
using KantorClient.Application.ViewModels.Interfaces.Transfers;
using KantorClient.Application.ViewModels.Interfaces.Users;
using KantorClient.Application.Views;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.Common.Extentions;
using KantorClient.DAL.Repositories;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;
using KantorServer.Model.Consts;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged, IMainWindowViewModel
    {
        private readonly ISettingsService _settingService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfigurationRepository _configurationRepository;

        public event PropertyChangedEventHandler? PropertyChanged;

        #region MainViews
        public MainWindowView FormType { get; private set; }

        public IRatesMainViewModel RatesMainVM { get; private set; }

        public ITransactionsMainViewModel TransactionsMainVM { get; private set; }

        public IUsersMainViewModel UsersMainVM { get; private set; }

        public ITransfersMainViewModel TransfersMainVM { get; private set; }

        public ICashRegistryMainViewModel CashRegistryMainVM { get; private set; }

        public IReportsMainViewModel ReportsMainVM { get; private set; }
        #endregion

        public MainWindowViewModel(ISettingsService settingsService, IAuthenticationService authenticationService, IConfigurationRepository configurationRepository, IRatesMainViewModel ratesMainVM,
                                   ITransactionsMainViewModel transactionsMainVM, IUsersMainViewModel usersMainViewModel,
                                   ITransfersMainViewModel transfersMainViewModel, ICashRegistryMainViewModel cashRegistryMainViewModel,
                                   IReportsMainViewModel reportsMainViewModel)
        {
            _settingService = settingsService;
            _authenticationService = authenticationService;
            _configurationRepository = configurationRepository;

            _authenticationService.OnlineModeChanged += AuthenticationService_OnlineModeChanged;

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

            ReportsMainVM = reportsMainViewModel;
            ReportsMainVM.Parent = this;

            RatesMainViewCommand = new DelegateCommand(RatesMainView);
            TransactionsMainViewCommand = new DelegateCommand(TransactionsMainView);
            UsersMainViewCommand = new DelegateCommand(UsersMainView);
            TransfersMainViewCommand = new DelegateCommand(TransfersMainView);
            CashRegistryMainViewCommand = new DelegateCommand(CashRegistryMainView);
            ReportsMainViewCommand = new DelegateCommand(ReportsMainView);
            LoginCommand = new DelegateCommand(Login);
            LogoutCommand = new DelegateCommand(Logout);
            ClosingCommand = new DelegateCommand<CancelEventArgs>(Closing);
            EditPlnCommand = new DelegateCommand(EditPln);

            _authenticationService.CashUpdated += _authenticationService_CashUpdated;
            _settingService.DataUpdated += _settingService_DataUpdated;
        }

        private void _settingService_DataUpdated(object sender, DateTime? newValue)
        {
            if (newValue != null)
            {
                LastUpdated = newValue.Value;
            }
        }

        private void AuthenticationService_OnlineModeChanged(object sender, bool newValue)
        {
            OnlineMode = newValue;
        }

        private void _authenticationService_CashUpdated(object sender, decimal newValue)
        {
            Cash = newValue;
        }

        #region Properties
        public Window Parent { get; set; }

        public bool LoggedOut { get; set; }

        public decimal Cash { get; set; }

        public DateTime LastUpdated { get; set; } 

        public string OnlineText => OnlineMode == false ? "Offline" : "Online";
        public bool OnlineMode { get; set; }

        public UserSession Session { get; set; }

        // Permissions
        public bool CanUseRate { get; private set; }
        public bool CanUseTransaction { get; private set; }
        public bool CanUseTransfer { get; private set; }
        public bool CanUseUsers { get; private set; }
        public bool CanUseCashRegistry { get; private set; }
        public bool CanUseReports { get; private set; }

        #endregion
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
            await ReportsMainVM.Load(loaded);

            CanUseRate = Session.HasUserPermission(PermissionKeys.Rate.ListRate);
            CanUseTransaction = Session.HasUserPermission(PermissionKeys.Transaction.ListTransaction);
            CanUseTransfer = Session.HasUserPermission(PermissionKeys.Transfer.ListTransfer);
            CanUseUsers = Session.HasUserPermission(PermissionKeys.User.ListUser);
            CanUseReports = Session.HasUserPermission(PermissionKeys.Report.ListReport);
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
            RatesMainVM.OnShow();
        }

        public ICommand TransactionsMainViewCommand { get; private set; }
        private void TransactionsMainView()
        {
            FormType = MainWindowView.TransactionList;
            TransactionsMainVM.OnShow();
        }

        public ICommand UsersMainViewCommand { get; private set; }
        private void UsersMainView()
        {
            FormType = MainWindowView.Users;
            UsersMainVM.OnShow();
        }

        public ICommand TransfersMainViewCommand { get; private set; }
        private void TransfersMainView()
        {
            FormType = MainWindowView.Transfers;
            TransfersMainVM.OnShow();
        }

        public ICommand CashRegistryMainViewCommand { get; private set; }
        private void CashRegistryMainView()
        {
            FormType = MainWindowView.CashRegistry;
            CashRegistryMainVM.OnShow();
        }

        public ICommand ReportsMainViewCommand { get; private set; }
        private void ReportsMainView()
        {
            FormType = MainWindowView.Reports;
            ReportsMainVM.OnShow();
        }

        public ICommand LoginCommand { get; private set; }
        private void Login()
        {
            var login = new LoginView(null, _authenticationService, _configurationRepository, false);
            login.ShowDialog();
        }

        public ICommand LogoutCommand { get; private set; }
        private void Logout()
        {
            this.LoggedOut = true;
            _authenticationService.SetOnlineMode(false);
            Parent.Close();
        }

        public ICommand EditPlnCommand { get; private set; }
        private void EditPln()
        {
            CashRegistryMainView();
            CashRegistryMainVM.SetPLN();
        }

        public ICommand ClosingCommand { get; private set; }
        private void Closing(CancelEventArgs e)
        {
            if (this.LoggedOut)
                return;
            var closingWindow = new ClosingWindow();
            closingWindow.ShowDialog();
            if (closingWindow.Action == 1)
            {
                this.LoggedOut = true;
                _authenticationService.SetOnlineMode(false);
            }
            else if (closingWindow.Action == 0)
            {
                e.Cancel = true;
            }
        }
        #endregion
    }
}
