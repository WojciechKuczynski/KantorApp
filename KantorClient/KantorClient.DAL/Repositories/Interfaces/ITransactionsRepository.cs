using KantorClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
