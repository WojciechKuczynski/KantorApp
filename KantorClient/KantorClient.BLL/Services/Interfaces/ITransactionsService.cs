using KantorClient.BLL.Models;
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
    }
}
