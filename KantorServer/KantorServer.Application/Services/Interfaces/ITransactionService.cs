using KantorServer.Application.Requests.Transactions;
using KantorServer.Model.Dtos;

namespace KantorServer.Application.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionDto>> AddTransactions(List<TransactionDto> transactions);
        Task<TransactionDto> SynchronizeTransaction(TransactionDto transaction, string notificationKey);

        Task<List<TransactionDto>> GetTransactions(GetTransactionsRequest request);
    }
}
