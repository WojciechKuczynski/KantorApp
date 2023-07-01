using KantorClient.BLL.Models;
using KantorClient.DAL.RequestArgs;
using KantorClient.Model;

namespace KantorClient.BLL.Services.Interfaces
{
    public interface ITransactionsService
    {
        Task<List<TransactionModel>> GetLocalTransactions();
        Task<TransactionModel> AddTransaction(TransactionModel transaction, UserSession userSession);
        Task<TransactionModel> EditTransaction(TransactionModel transaction, UserSession userSession);
        Task<bool> DeleteTransaction(TransactionModel transaction);

    }
}
