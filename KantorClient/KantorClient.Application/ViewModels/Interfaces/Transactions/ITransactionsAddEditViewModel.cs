using KantorClient.Application.ViewModels.Interfaces.Rates;
using KantorClient.BLL.Models;
using KantorClient.Model.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Transactions
{
    public interface ITransactionsAddEditViewModel
    {
        ITransactionsMainViewParent Parent { get; set; }
        Task Load();
        void LoadForm(TransactionModel model = null, TransactionType type = TransactionType.Sell);
    }
}
