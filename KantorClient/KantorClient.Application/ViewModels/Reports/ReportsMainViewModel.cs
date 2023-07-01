using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Reports;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.RequestArgs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Reports
{
    public class ReportsMainViewModel : IReportsMainViewModel, INotifyPropertyChanged
    {
        private readonly IReportsService _reportsService;
        private TransactionReportModel _selectedTransaction;

        public ReportsMainViewModel(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }
        public IMainWindowContainer Parent { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public bool ContextOpened { get; set; }
        public List<UserModelLight> Users { get; set; }
        public List<KantorModel> Kantors { get; set; }
        public List<CurrencyModel> Currencies { get; set; }
        public ObservableCollection<TransactionReportModel> Transactions { get; set; }
        public TransactionReportModel SelectedTransaction
        {
            get { return _selectedTransaction; }
            set
            {
                _selectedTransaction = value;
                ContextOpened = value.Parent != null;
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
            }
        }

        public async Task OnShow()
        {
            Refresh();
        }

        private async void Refresh()
        {
            try
            {
                var request = new TransactionsRequestArgs
                {

                };
                var trans = await _reportsService.GetTransactions(request);
                if (trans != null)
                {
                    TransactionList = trans;
                    //foreach(var transaction in TransactionList)
                    //{
                    //    transaction.Edited = false;
                    //    if (transaction.Parent != null && TransactionList.Any(y => y.ExternalId == transaction.Parent))
                    //    {
                    //        transaction.Edited = true;
                    //    }
                    //}
                    Transactions = new ObservableCollection<TransactionReportModel>(TransactionList.Where(x =>x .Edited == false));
                }
            }
            catch
            {

            }
        }
    }
}
