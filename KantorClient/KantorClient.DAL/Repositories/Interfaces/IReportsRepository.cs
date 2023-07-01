using KantorClient.DAL.ResponseArgs;
using KantorServer.Application.Requests.Transactions;
using KantorServer.Model.Dtos;

namespace KantorClient.DAL.Repositories.Interfaces
{
    public interface IReportsRepository
    {
        Task<ReportsSettingsResponseArgs> GetReportsSettings(string synchronizationKey);

        Task<List<TransactionDto>> GetTransactions(GetTransactionsRequest request);
    }
}
