using KantorClient.Application.Models;
using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Reports;
using KantorClient.BLL.Models;
using KantorClient.BLL.Printing;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.RequestArgs;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Reports
{
    public class ReportsMainViewModel : IReportsMainViewModel, INotifyPropertyChanged
    {
        private readonly IReportsService _reportsService;
        private readonly ISettingsService _settingsSerivce;
        private TransactionReportModel _selectedTransaction;

        public ReportsMainViewModel(IReportsService reportsService, ISettingsService settingsService)
        {
            _reportsService = reportsService;
            _settingsSerivce = settingsService;

            RefreshCommand = new DelegateCommand(Refresh);
            PrintCommand = new DelegateCommand(Print);
        }
        public IMainWindowContainer Parent { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public bool ContextOpened { get; set; }
        public ObservableCollection<ComboBoxItem> KantorsList { get; set; }
        public ObservableCollection<ComboBoxItem> UsersList { get; set; }
        public ObservableCollection<ComboBoxItem> CurrenciesList { get; set; }
        public List<UserModelLight> Users { get; set; }
        public List<KantorModel> Kantors { get; set; }
        public List<CurrencyModel> Currencies { get; set; }
        public ObservableCollection<TransactionReportModel> Transactions { get; set; }
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

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task Load(bool loaded = false)
        {
            var settings = await _reportsService.GetReportsSettings();
            if (settings != null)
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
                    Transactions = new ObservableCollection<TransactionReportModel>(TransactionList.Where(x => x.Edited == false));
                }
            }
            catch
            {

            }
        }

        public ICommand PrintCommand { get; private set; }
        private async void Print()
        {
            //var saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.Filter = "excel files|*.xlsx";
            //saveFileDialog1.Title = "Choose file to save";
            //saveFileDialog1.ShowDialog();

            //if (saveFileDialog1.FileName != "")
            //{
            //    PrintingModule.ExportToExcel(Transactions.ToList(), saveFileDialog1.FileName);
            //}
            PrintingModule.PrintTest();
        }
    }
}
