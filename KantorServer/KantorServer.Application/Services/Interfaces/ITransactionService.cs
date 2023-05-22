using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionDto>> AddTransactions(List<TransactionDto> transactions);
        Task<TransactionDto> SynchronizeTransaction(TransactionDto transaction, string notificationKey);
    }
}
