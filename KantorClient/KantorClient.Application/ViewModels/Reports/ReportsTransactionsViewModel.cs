using KantorClient.Application.Models;
using KantorClient.Application.ViewModels.Interfaces.Reports;
using KantorClient.BLL.Models;
using KantorClient.BLL.Printing;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.RequestArgs;
using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Reports
{
    public class ReportsTransactionsViewModel : IReportsTransactionsViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        #region dependencies

        private readonly IReportsService _reportsService;
        private readonly ISettingsService _settingsSerivce;

        #endregion

        #region private properties

        private TransactionReportModel _selectedTransaction;
        private ObservableCollection<TransactionReportModel> _selectedTransactions;


        #endregion

        public ReportsTransactionsViewModel(IReportsService reportsService, ISettingsService settingsService)
        {
            _reportsService = reportsService;
            _settingsSerivce = settingsService;

            RefreshCommand = new DelegateCommand(Refresh);
            PrintCommand = new DelegateCommand(Print);
            SelectItemsCommand = new DelegateCommand<object>(SelectItems);
            _selectedTransactions = new ObservableCollection<TransactionReportModel>();
        }

        #region Methods

        public async Task Load(bool loaded = false)
        {
            var settings = await _reportsService.GetReportsSettings();
            if (settings.Users != null && settings?.Kantors != null)
            {
                Users = settings.Users.Select(x => new UserModelLight { Id = x.Id, Name = x.Name }).ToList();
                Kantors = settings.Kantors.Select(x => new KantorModel { Id = x.Id, Name = x.Name }).ToList();
                Currencies = _settingsSerivce.Currencies.Select(x => new CurrencyModel(x)).ToList();

                KantorsList = new ObservableCollection<ComboBoxItem>(Kantors.Select(x => new ComboBoxItem { Selected = false, Object = x }));
                UsersList = new ObservableCollection<ComboBoxItem>(Users.Select(x => new ComboBoxItem { Selected = false, Object = x }));
                CurrenciesList = new ObservableCollection<ComboBoxItem>(Currencies.Select(x => new ComboBoxItem { Selected = false, Object = x }));
            }
        }

        public Task OnShow()
        {
            Refresh();
            return Task.CompletedTask;
        }


        #endregion

        #region Properties

        // filters
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public ObservableCollection<ComboBoxItem> KantorsList { get; set; }
        public ObservableCollection<ComboBoxItem> UsersList { get; set; }
        public ObservableCollection<ComboBoxItem> CurrenciesList { get; set; }
        public List<UserModelLight> Users { get; set; }
        public List<KantorModel> Kantors { get; set; }
        public List<CurrencyModel> Currencies { get; set; }

        //DataGrid
        public bool ContextOpened { get; set; }
        public decimal QuantitySum { get; set; }
        public decimal ValueSum { get; set; }
        public List<TransactionReportModel> Transactions { get; set; }
        public ObservableCollection<TransactionReportModel> SelectedTransactions
        {
            get { return _selectedTransactions; }
            set { _selectedTransactions = value; }
        }
        public TransactionReportModel ParentTransaction => TransactionList.FirstOrDefault(x => x.ExternalId == SelectedTransaction?.Parent) ?? SelectedTransaction;
        public TransactionReportModel SelectedTransaction
        {
            get { return _selectedTransaction; }
            set
            {
                _selectedTransaction = value;
                ContextOpened = value?.Parent != null;
            }
        }
        private List<TransactionReportModel> TransactionList { get; set; }


        //Other
        public bool ShowDeleted { get; set; }
        public bool Loading { get; set; }

        #endregion

        #region Commands

        public ICommand RefreshCommand { get; private set; }
        private async void Refresh()
        {
            try
            {
                var request = new TransactionsRequestArgs
                {
                    DateFrom = this.DateFrom,
                    DateTo = this.DateTo,
                    Currencies = CurrenciesList.Where(x => x.Selected).Select(x => (x.Object as CurrencyModel).Symbol),
                    Kantors = KantorsList.Where(x => x.Selected).Select(x => (x.Object as KantorModel).Id),
                    Users = UsersList.Where(x => x.Selected).Select(x => (x.Object as UserModelLight).Id),
                };
                var trans = await _reportsService.GetTransactions(request);
                if (trans != null)
                {
                    TransactionList = trans;
                    Transactions = new List<TransactionReportModel>(TransactionList.Where(x => x.Edited == false && (x.Valid || ShowDeleted)));
                    QuantitySum = Transactions.Where(x => x.Valid).Sum(x => x.Quantity);
                    ValueSum = Transactions.Where(x => x.Valid).Sum(x => x.FinalValue);
                }
            }
            catch
            {

            }
        }

        public ICommand PrintCommand { get; private set; }
        private async void Print()
        {
            try
            {
                Loading = true;
                var saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files|*.xlsx";
                saveFileDialog1.Title = "Choose file to save";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "")
                {
                    PrintingModule.ExportToExcel(Transactions.ToList(), saveFileDialog1.FileName);
                }
            }
            finally
            {
                Loading = false;
            }
        }

        public ICommand SelectItemsCommand { get; private set; }
        private void SelectItems(object models)
        {
            try
            {
                if (models is ObservableCollection<object> modelList)
                {
                    SelectedTransactions = new ObservableCollection<TransactionReportModel>(modelList.Cast<TransactionReportModel>());
                }
            }
            catch { }
        }

        #endregion
    }
}
