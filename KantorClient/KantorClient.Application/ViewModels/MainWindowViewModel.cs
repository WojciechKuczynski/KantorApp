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
        #endregion
        public MainWindowViewModel(ISettingsService settingsService, IRatesMainViewModel ratesMainVM, ITransactionsMainViewModel transactionsMainVM)
        {
            _settingService = settingsService;

            RatesMainVM = ratesMainVM;
            RatesMainVM.Parent = this;

            TransactionsMainVM = transactionsMainVM;
            TransactionsMainVM.Parent = this;

            RatesMainViewCommand = new DelegateCommand(RatesMainView);
            TransactionsMainViewCommand = new DelegateCommand(TransactionsMainView);
        }

        public Window Parent { get; set; }

        public bool LoggedOut { get; set; }

        public async Task Load()
        {
            var loaded = await _settingService.LoadSettings();
            await RatesMainVM.Load(loaded);
            await TransactionsMainVM.Load(loaded);
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
        #endregion
    }
}
