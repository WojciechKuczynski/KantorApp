using KantorClient.BLL.Models;
using KantorClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
