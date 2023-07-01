using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;
using Microsoft.EntityFrameworkCore;

namespace KantorClient.DAL.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            using var dataContext = new DataContext();
            transaction.Currency = await dataContext.Currencies.FindAsync(transaction.Currency.Id);
            transaction.User = await dataContext.UserSessions.FindAsync(transaction.User.Id);
            if (transaction.User == null || transaction.Currency == null) 
            {
                return null;
            }

            transaction.TransactionDate = DateTime.Now;
            transaction.Valid = true;
            transaction.Synchronized = false;
            await dataContext.Transactions.AddAsync(transaction);
            await dataContext.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction> DeleteTransaction(Transaction transaction)
        {
            using var dataContext = new DataContext();
            var transactionInDb = await dataContext.Transactions.FindAsync(transaction.Id);
            if (transactionInDb == null)
            {
                return null;
            }

            transactionInDb.Valid = false;
            transactionInDb.DeletionDate = DateTime.Now;
            transactionInDb.Synchronized = false;
            await dataContext.SaveChangesAsync();
            return transactionInDb;
        }

        public async Task<Transaction> EditTransaction(Transaction transaction)
        {
            using var dataContext = new DataContext();
            var transactionInDb = await dataContext.Transactions.FindAsync(transaction.Id);
            if (transactionInDb == null)
            {
                return null;
            }
            // delete old
            transactionInDb.Valid = false;
            transactionInDb.DeletionDate = DateTime.Now;
            transactionInDb.Synchronized = false;
            transactionInDb.Edited = true;

            //new
            var newTransaction = new Transaction()
            {
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                FinalValue = transaction.FinalValue,
                Rate = transaction.Rate,
                Parent = transactionInDb.Id,
                Valid = true,
                Synchronized = false,
                TransactionDate = DateTime.UtcNow,
        };
            newTransaction.Currency = await dataContext.Currencies.FindAsync(transaction.Currency.Id);
            newTransaction.User = await dataContext.UserSessions.FindAsync(transaction.User.Id);

            await dataContext.AddAsync(newTransaction);
            await dataContext.SaveChangesAsync();
            return newTransaction;
        }

        public async Task<IEnumerable<Transaction>> GetLocalTransactions()
        {
            using var dataContext = new DataContext();
            var localTransactions = await dataContext.Transactions.Include(x => x.Currency).Include(x => x.User).Where(x => x.DeletionDate == null || (x.DeletionDate != null && x.Parent == null)).OrderByDescending(x => x.TransactionDate).Take(100).ToListAsync();

            return localTransactions;
        }
    }
}
