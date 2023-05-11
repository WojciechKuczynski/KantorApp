using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.Repositories.Interfaces;

namespace KantorClient.BLL.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionsService(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        public async Task<List<TransactionModel>> GetLocalTransactions()
        {
            try
            {
                var transactions = await _transactionsRepository.GetLocalTransactions();
                if (transactions == null)
                {
                    return new List<TransactionModel>();
                }

                var transactionModels = transactions.Select(x => new TransactionModel(x));
                return transactionModels.ToList();
            }
            catch (Exception ex)
            {
                //TODO: logging
            }
            return new List<TransactionModel>();
        }
    }
}
