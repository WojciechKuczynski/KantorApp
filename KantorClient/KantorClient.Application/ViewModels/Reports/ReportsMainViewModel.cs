using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Reports;
using KantorClient.BLL.Services.Interfaces;
using Prism.Commands;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Reports
{
    public class ReportsMainViewModel : IReportsMainViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ReportsMainViewModel(IReportsService reportsService, ISettingsService settingsService)
        {
            TransactionsVM = new ReportsTransactionsViewModel(reportsService, settingsService);
            UsersVM = new ReportsUsersViewModel(reportsService, settingsService);

            SetTransactionCommand = new DelegateCommand(SetTransactions);
            SetUsersCommand = new DelegateCommand(SetUsers);
        }

        #region Reports VMS
        public IReportsTransactionsViewModel TransactionsVM { get; set; }
        public IReportsUsersViewModel UsersVM { get; set; }

        #endregion

        public IMainWindowContainer Parent { get; set; }


        #region VisibilityProperties

        public bool TransactionsVisible { get; set; }
        public bool UsersVisible { get; set; }

        #endregion

        #region Methods

        public async Task Load(bool loaded = false)
        {
            await TransactionsVM.Load(loaded);
            await UsersVM.Load(loaded);
        }

        public async Task OnShow()
        {
            await TransactionsVM.OnShow();
        }

        private void ResetVisibilities()
        {
            TransactionsVisible = false;
            UsersVisible = false;
        }

        #endregion

        #region Commands

        public ICommand SetTransactionCommand { get; private set; }
        private void SetTransactions()
        {
            if (!TransactionsVisible)
            {
                ResetVisibilities();
                TransactionsVisible = true;
            }
        }

        public ICommand SetUsersCommand { get; private set; }
        private void SetUsers()
        {
            if (!UsersVisible)
            {
                ResetVisibilities();
                UsersVisible = true;
            }
        }

        #endregion
    }
}
