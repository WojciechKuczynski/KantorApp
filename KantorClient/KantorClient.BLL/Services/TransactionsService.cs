using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;

namespace KantorClient.BLL.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICashRegistryService _cashRegistryService;
        private readonly IConfigurationRepository _configurationRepository;

        public TransactionsService(ITransactionsRepository transactionsRepository, IAuthenticationService authenticationService, ICashRegistryService cashRegistryService, IConfigurationRepository configurationRepository)
        {
            _transactionsRepository = transactionsRepository;
            _authenticationService = authenticationService;
            _cashRegistryService = cashRegistryService;
            _configurationRepository = configurationRepository;
        }

        public async Task<TransactionModel> AddTransaction(TransactionModel transaction, UserSession userSession)
        {
            try
            {
                var trans = transaction.Map();
                trans.User = userSession;
                var addedTrans = await _transactionsRepository.AddTransaction(trans);
                await HandleCashRegistry(addedTrans);
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
                var deletedTrans = await _transactionsRepository.DeleteTransaction(trans);
                deletedTrans.FinalValue *= -1; // when deleted we want to negate cashRegistry update
                await HandleCashRegistry(deletedTrans);
                return deletedTrans != null;
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
                await HandleCashRegistry(editedTrans);
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

        private async Task HandleCashRegistry(Transaction trans)
        {
            if (trans == null)
                return;

            switch (trans.TransactionType)
            {
                case Model.Consts.TransactionType.Buy:
                    await _authenticationService.AddPln((-1) * trans.FinalValue);
                    await _cashRegistryService.AddCurrency(trans.Currency, trans.Quantity);
                    break;
                case Model.Consts.TransactionType.Sell:
                    await _authenticationService.AddPln(trans.FinalValue);
                    await _cashRegistryService.AddCurrency(trans.Currency, (-1) * trans.Quantity);
                    break;
            }
        }
    }
}
