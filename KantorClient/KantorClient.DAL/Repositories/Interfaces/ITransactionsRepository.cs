using KantorClient.Model;

namespace KantorClient.DAL.Repositories.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<Transaction>> GetLocalTransactions();
        Task<Transaction> AddTransaction(Transaction transaction);
        Task<Transaction> DeleteTransaction(Transaction transaction);
        Task<Transaction> EditTransaction(Transaction transaction);
    }
}
