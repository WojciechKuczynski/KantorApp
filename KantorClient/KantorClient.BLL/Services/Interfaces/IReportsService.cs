using KantorClient.BLL.Models;
using KantorClient.DAL.RequestArgs;
using KantorClient.DAL.ResponseArgs;

namespace KantorClient.BLL.Services.Interfaces
{
    public interface IReportsService
    {
        Task<ReportsSettingsResponseArgs> GetReportsSettings();
        Task<List<TransactionReportModel>> GetTransactions(TransactionsRequestArgs request);
    }
}
