﻿using KantorClient.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Transactions
{
    public interface ITransactionsMainViewParent
    {
        void CancelForm();
        Task<bool> AddTransaction(TransactionModel transactionModel);
        Task<bool> EditTransaction(TransactionModel transactionModel);
    }
}
