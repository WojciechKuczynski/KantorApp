﻿using KantorServer.Application.Services.Interfaces;
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

        public async Task<TransactionDto> SynchronizeTransaction(TransactionDto transaction, string notificationKey)
        {
            try
            {
                var transactionInDb = await DataContext.Transactions.FirstOrDefaultAsync(x => x.ExternalId == transaction.ExternalId);
                if (transactionInDb != null)
                {
                    // EDIT
                    return await EditTransaction(transaction, transactionInDb, notificationKey);
                }

                // ADD
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
                transactionForDb.Currency = await DataContext.Currencies.FirstOrDefaultAsync(x => x.Symbol == transaction.Currency.Symbol);
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

        private async Task<TransactionDto> EditTransaction(TransactionDto transaction,Transaction transactionInDb, string notificationKey)
        {
            try
            {
                var userSession = await DataContext.UserSessions
                                .Include(x => x.Kantor)
                                .Include(x => x.User)
                                .FirstOrDefaultAsync(x => x.SynchronizationKey == notificationKey);

                if (userSession == null)
                {
                    return null;
                }

                transactionInDb.User = userSession.User;
                transactionInDb.Kantor = userSession.Kantor;
                transactionInDb.Currency = await DataContext.Currencies.FirstOrDefaultAsync(x => x.Symbol == transaction.Currency.Symbol);
                transactionInDb.TransactionDate = transaction.TransactionDate;
                transactionInDb.TransactionType = transaction.TransactionType;
                transactionInDb.DeletionDate = transaction.DeletionDate;
                transactionInDb.FinalValue = transaction.FinalValue;
                transactionInDb.Quantity = transaction.Quantity;
                transactionInDb.Rate = transaction.Rate;
                transactionInDb.Parent = transaction.Parent;
                transactionInDb.Valid = transaction.Valid;
                transactionInDb.Edited = transaction.Edited;

                if (transaction.Parent != null)
                {
                    // where transactionId from Kantor equals Parent value
                    var parent = await DataContext.Transactions
                                    .Include(x => x.Kantor)
                                    .FirstOrDefaultAsync(x => x.ExternalId == transaction.Parent.Value && x.Kantor.Id == userSession.Kantor.Id);
                    if (parent != null)
                    {
                        transaction.Parent = parent.Id;
                    }
                }

                await DataContext.SaveChangesAsync();
                return new TransactionDto(transactionInDb);
            }
            catch (Exception ex) { }
            return null;
        }
    }
}