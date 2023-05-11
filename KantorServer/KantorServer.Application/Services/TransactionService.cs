using KantorServer.Application.Services.Interfaces;
using KantorServer.DAL;
using KantorServer.Model;
using KantorServer.Model.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Services
{
    public class TransactionService : ITransactionService
    {
        public DataContext DataContext { get; }

        public TransactionService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<List<TransactionDto>> AddTransactions(List<TransactionDto> transactions)
        {
            try
            {
                var transactionsForDb = transactions.Select(x => x.ConvertToEntity());
                await DataContext.Transactions.AddRangeAsync(transactionsForDb);
                await DataContext.SaveChangesAsync();

                var addedTransactions = TransactionDto.Map(transactionsForDb);
                return addedTransactions;
            }
            catch (Exception ex) 
            {
                return null;
            }
        }

        public async Task<TransactionDto> AddTransaction(TransactionDto transaction, string notificationKey)
        {
            try
            {
                var transactionForDb = transaction.ConvertToEntity();
                var userSession = await DataContext.UserSessions
                                    .Include(x => x.Kantor)
                                    .Include(x => x.User)
                                    .FirstOrDefaultAsync(x => x.SynchronizationKey == notificationKey);
                if (userSession == null) 
                {
                    return null;
                }

                transactionForDb.User = userSession.User;
                transactionForDb.Kantor = userSession.Kantor;

                if (transaction.Parent != null)
                {
                    var parent = await DataContext.Transactions
                                            .Include(x => x.Kantor)
                                            .FirstOrDefaultAsync(x => x.ExternalId == transaction.Parent.Value && x.Kantor.Id == userSession.Kantor.Id);
                    if (parent != null)
                    {
                        transaction.Parent = parent.Id;
                    }
                }

                await DataContext.Transactions.AddRangeAsync(transactionForDb);
                await DataContext.SaveChangesAsync();

                var addedTransaction = new TransactionDto(transactionForDb);
                return addedTransaction;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
