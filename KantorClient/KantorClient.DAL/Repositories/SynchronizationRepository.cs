﻿using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.DAL.ServerCommunication;
using KantorClient.Model;
using KantorServer.Application.Requests;
using KantorServer.Application.Requests.Rates;
using KantorServer.Application.Requests.Transactions;
using KantorServer.Application.Responses;
using KantorServer.Application.Responses.Rates;
using KantorServer.Application.Responses.Transactions;
using KantorServer.Model.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.DAL.Repositories
{
    public class SynchronizationRepository : ISynchronizationRepository
    {
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
                catch (Exception ex)
                {

                }
            }
        }

        private async Task<SynchronizeTransactionResponse> SendTransactionRequest(SynchronizeTransactionRequest request)
        {
            var requestContext = new RequestContext("https://localhost:7254/transactions/synchronize", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<SynchronizeTransactionRequest, SynchronizeTransactionResponse>(requestContext, request);
            return response;
        }

        private async Task<AddEditRateResponse> SendRateRequest(AddEditRateRequest request)
        {
            var requestContext = new RequestContext("https://localhost:7254/rates/addRate", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<AddEditRateRequest, AddEditRateResponse>(requestContext, request);
            return response;
        }
    }
}