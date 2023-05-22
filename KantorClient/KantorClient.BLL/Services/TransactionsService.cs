using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;

namespace KantorClient.BLL.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionsService(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        public async Task<TransactionModel> AddTransaction(TransactionModel transaction, UserSession userSession)
        {
            try
            {
                var trans = transaction.Map();
                trans.User = userSession;
                var addedTrans = await _transactionsRepository.AddTransaction(trans);
                return new TransactionModel(addedTrans);
            }
            catch { }
            return null;
        }

        public async Task<bool> DeleteTransaction(TransactionModel transaction)
        {
            try
            {
                var trans = transaction.Map();
                var addedTrans = await _transactionsRepository.DeleteTransaction(trans);
                return addedTrans != null;
            }
            catch { }
            return false;
        }

        public async Task<TransactionModel> EditTransaction(TransactionModel transaction, UserSession userSession)
        {
            try
            {
                var trans = transaction.Map();
                trans.User = userSession;
                var editedTrans = await _transactionsRepository.EditTransaction(trans);
                if (editedTrans == null)
                    return null;
                return new TransactionModel(editedTrans);
            }
            catch { }
            return null;
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
