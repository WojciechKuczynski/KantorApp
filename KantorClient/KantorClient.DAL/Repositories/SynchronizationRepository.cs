using KantorClient.Common.Exceptions;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.DAL.ServerCommunication;
using KantorServer.Application.Requests.Rates;
using KantorServer.Application.Requests.Transactions;
using KantorServer.Application.Requests.Transfers;
using KantorServer.Application.Responses.Rates;
using KantorServer.Application.Responses.Transactions;
using KantorServer.Application.Responses.Transfers;
using KantorServer.Model.Dtos;
using Microsoft.EntityFrameworkCore;

namespace KantorClient.DAL.Repositories
{
    public class SynchronizationRepository : ISynchronizationRepository
    {
        private readonly IConfigurationRepository _configurationRepository;
        public SynchronizationRepository(IConfigurationRepository configurationRepository)
        {

            _configurationRepository = configurationRepository;

        }
        public async Task SynchronizeRate(string synchronizationToken)
        {
            using (var dataContext = new DataContext())
            {
                try
                {
                    var transactionsForSynchro = dataContext.Rates.Include(x => x.Currency)
                        .Where(x => x.Synchronized == false) // not synchronized
                        .OrderBy(x => x.Id).Take(5);
                    foreach (var t in transactionsForSynchro)
                    {
                        var request = new AddEditRateRequest()
                        {
                            SynchronizationKey = synchronizationToken,
                            Rate = new RateDto()
                            {
                                ExternalId = t.Id,
                                DefaultBuyRate = t.DefaultBuyRate,
                                DefaultSellRate = t.DefaultSellRate,
                                MaximumBuyRate = t.MaximumBuyRate,
                                MinimalSellRate = t.MinimalSellRate,
                                StartDate = t.StartDate,
                                EndDate = t.EndDate,
                                UseNbpSpread = t.UseNbpSpread,
                                Spread = t.Spread,
                                Currency = new CurrencyDto { Name = t.Currency.Name, Symbol = t.Currency.Symbol },
                            }
                        };
                        var result = await SendRateRequest(request);
                        if (result != null)
                        {
                            t.ExternalId = result.Rate.Id;
                            t.Synchronized = true;
                            await dataContext.SaveChangesAsync();
                        }
                    }
                }
                catch (ServerNotReachedException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task SynchronizeTransactions(string synchronizationToken)
        {
            using (var dataContext = new DataContext())
            {
                try
                {
                    var transactionsForSynchro = dataContext.Transactions
                        .Include(x => x.Currency)
                        .Where(x => x.Synchronized == false) // not synchronized
                        .OrderBy(x => x.Id).Take(5);
                    foreach (var t in transactionsForSynchro)
                    {
                        if (t.Parent.HasValue)
                        {
                            var parent = await dataContext.Transactions.FirstOrDefaultAsync(x => x.Id == t.Parent);
                            if (parent != null)
                            {
                                t.Parent = parent.Id;
                            }
                        }
                        var request = new SynchronizeTransactionRequest()
                        {
                            SynchronizationKey = synchronizationToken,
                            Transaction = new TransactionDto()
                            {
                                ExternalId = t.Id,
                                FinalValue = t.FinalValue,
                                Parent = t.Parent,
                                Quantity = t.Quantity,
                                TransactionType = (KantorServer.Model.Consts.TransactionType)t.TransactionType,
                                Currency = new CurrencyDto { Name = t.Currency.Name, Symbol = t.Currency.Symbol },
                                Rate = t.Rate,
                                TransactionDate = t.TransactionDate,
                                DeletionDate = t.DeletionDate,
                                Valid = t.Valid,
                                Edited = t.Edited,

                                // User and Kantor taken from synchroKey
                                User = new UserDto(),
                                Kantor = new KantorDto()
                            }
                        };
                        var result = await SendTransactionRequest(request);
                        if (result != null)
                        {
                            t.ExternalId = result.Transaction.Id;
                            t.Synchronized = true;
                            await dataContext.SaveChangesAsync();
                        }
                    }
                }
                catch(ServerNotReachedException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task SynchronizeTransfers(string synchronizationToken)
        {
            using (var dataContext = new DataContext())
            {
                try
                {
                    var transactionsForSynchro = dataContext.Transfers
                        .Include(x => x.TransferCurrency)
                        .Where(x => x.Synchronized == false) // not synchronized
                        .OrderBy(x => x.Id).Take(5);

                    foreach (var t in transactionsForSynchro)
                    {
                        if (t.Parent.HasValue)
                        {
                            var parent = await dataContext.Transactions.FirstOrDefaultAsync(x => x.Id == t.Parent);
                            if (parent != null)
                            {
                                t.Parent = parent.Id;
                            }
                        }
                        var request = new SynchronizeTransferRequest()
                        {
                            SynchronizationKey = synchronizationToken,
                            Transfer = new TransferDto()
                            {
                                ExternalId = t.Id,
                                Parent = t.Parent,
                                TransferValue = t.TransferValue,
                                TransferType = (KantorServer.Model.Consts.TransferType)t.Type,
                                TransferCurrency = new CurrencyDto { Name = t.TransferCurrency.Name, Symbol = t.TransferCurrency.Symbol },
                                TransferDate = t.TransferDate,
                                DeletionDate = t.DeletionDate,
                                Notes = t.Notes,
                                Valid = t.Valid,
                                Edited = t.Edited,

                                // User and Kantor taken from synchroKey
                                User = new UserDto(),
                                Kantor = new KantorDto()
                            }
                        };
                        var result = await SendTransferRequest(request);
                        if (result != null)
                        {
                            t.ExternalId = result.Transfer.Id;
                            t.Synchronized = true;
                            await dataContext.SaveChangesAsync();
                        }
                    }
                }
                catch (ServerNotReachedException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private async Task<SynchronizeTransactionResponse> SendTransactionRequest(SynchronizeTransactionRequest request)
        {
            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/transactions/synchronize", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<SynchronizeTransactionRequest, SynchronizeTransactionResponse>(requestContext, request);
            if (response == null)
            {
                throw new ServerNotReachedException();
            }
            return response;
        }

        private async Task<SynchronizeTransferResponse> SendTransferRequest(SynchronizeTransferRequest request)
        {
            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/transfers/synchronize", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<SynchronizeTransferRequest, SynchronizeTransferResponse>(requestContext, request);
            if (response == null)
            {
                throw new ServerNotReachedException();
            }
            return response;
        }

        private async Task<AddEditRateResponse> SendRateRequest(AddEditRateRequest request)
        {
            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/rates/addRate", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<AddEditRateRequest, AddEditRateResponse>(requestContext, request);
            if (response == null)
            {
                throw new ServerNotReachedException();
            }
            return response;
        }
    }
}
