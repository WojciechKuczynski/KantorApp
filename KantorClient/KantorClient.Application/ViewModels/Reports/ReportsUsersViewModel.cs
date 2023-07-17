using KantorClient.Application.Consts;
using KantorClient.Application.ViewModels.Interfaces.Reports;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.RequestArgs;
using LiveCharts;
using LiveCharts.Wpf;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Reports
{
    public class ReportsUsersViewModel : IReportsUsersViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        #region dependencies

        private readonly IReportsService _reportsService;
        private readonly ISettingsService _settingsSerivce;

        #endregion

        public ReportsUsersViewModel(IReportsService reportsService, ISettingsService settingsService)
        {
            _reportsService = reportsService;
            _settingsSerivce = settingsService;

            RefreshCommand = new DelegateCommand<string>(Refresh);
        }

        #region Properties

        private List<TransactionReportModel> TransactionList { get; set; }
        public List<TransactionReportModel> Transactions { get; set; }

        public DateTime CustomDateFrom { get; set; }
        public DateTime CustomDateTo { get; set; }

        #endregion

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        #region Commands

        public ICommand RefreshCommand { get; private set; }
        private async void Refresh(string timeSpan)
        {
            try
            {
                var ts = int.Parse(timeSpan);
                var request = GenerateRequest((ReportTimespan)ts);

                var trans = await _reportsService.GetTransactions(request);
                if (trans != null)
                {
                    TransactionList = trans;
                    Transactions = new List<TransactionReportModel>(TransactionList.Where(x => x.Edited == false && x.Valid));
                    var groupedBuy = TransactionList.Where(x => x.Valid && x.TransactionType == Model.Consts.TransactionType.Buy).GroupBy(x => x.UserId).Select(x => new KeyValuePair<long, decimal>(x.Key, x.Sum(z => z.FinalValue))).ToList();
                    var groupedSell = TransactionList.Where(x => x.Valid && x.TransactionType == Model.Consts.TransactionType.Sell).GroupBy(x => x.UserId).Select(x => new KeyValuePair<long, decimal>(x.Key, x.Sum(z => z.FinalValue))).ToList();

                    var allUsers = groupedBuy.Select(x => x.Key).ToList();
                    allUsers.AddRange(groupedSell.Select(x => x.Key));
                    allUsers = allUsers.Distinct().ToList();

                    SeriesCollection = new SeriesCollection();
                    var sellColumnSeries = new ColumnSeries();
                    var buyColumnSeries = new ColumnSeries();
                    buyColumnSeries.Title = "KUPNO";
                    buyColumnSeries.Values = new ChartValues<decimal>();
                    sellColumnSeries.Title = "SPRZEDAŻ";
                    sellColumnSeries.Values = new ChartValues<decimal>();
                    var labels = new List<string>();
                    foreach(var user in allUsers)
                    {
                        var label = TransactionList.FirstOrDefault(x => x.UserId == user)?.UserName;
                        labels.Add(label ?? user.ToString());
                        var a = groupedBuy.Where(x => x.Key == user)?.Sum(x => x.Value);
                        buyColumnSeries.Values.Add(a ?? 0);
                        sellColumnSeries.Values.Add(groupedSell.Where(x => x.Key == user)?.Sum(x => x.Value) ?? 0);
                    }
                    SeriesCollection.Add(buyColumnSeries);
                    SeriesCollection.Add(sellColumnSeries);
                    Formatter = value => value.ToString("N");
                    Labels = labels.ToArray();
                }
            }
            catch
            {

            }
        }


        private TransactionsRequestArgs GenerateRequest(ReportTimespan timespan)
        {
            switch (timespan)
            {
                case ReportTimespan.Day:
                    return new TransactionsRequestArgs { DateFrom = DateTime.Now.Date };
                case ReportTimespan.Week:
                    return new TransactionsRequestArgs { DateFrom = DateTime.Now.Date.AddDays((-1) * (DateTime.Now.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)DateTime.Now.DayOfWeek - 1)) };
                case ReportTimespan.LastWeek:
                    var lastWeekStart = DateTime.Now.Date.AddDays(-7).AddDays((-1) * (DateTime.Now.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)DateTime.Now.DayOfWeek - 1));
                    return new TransactionsRequestArgs
                    {
                        DateFrom = lastWeekStart,
                        DateTo = lastWeekStart.AddDays(6)
                    };
                case ReportTimespan.Month:
                    return new TransactionsRequestArgs { DateFrom = DateTime.Now.Date.AddDays((-1) * DateTime.Now.Date.Day + 1) };
                case ReportTimespan.None:
                case ReportTimespan.Custom:
                default:
                    return new TransactionsRequestArgs { DateFrom = CustomDateFrom.Date, DateTo = CustomDateTo.Date };
            }
        }

        public Task Load(bool loaded = false)
        {
            CustomDateFrom = DateTime.Now.AddDays(-7);
            CustomDateTo = DateTime.Now;
            Refresh("1");
            return Task.CompletedTask;
        }
        #endregion
    }
}
