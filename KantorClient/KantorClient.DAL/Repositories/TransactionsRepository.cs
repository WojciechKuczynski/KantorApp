using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;
using Microsoft.EntityFrameworkCore;

namespace KantorClient.DAL.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        public async Task<IEnumerable<Transaction>> GetLocalTransactions()
        {
            using var dataContext = new DataContext();
            var localTransactions = await dataContext.Transactions.Include(x => x.Currency).Include(x => x.User).Take(100).ToListAsync();

            return localTransactions;
        }
    }
}
