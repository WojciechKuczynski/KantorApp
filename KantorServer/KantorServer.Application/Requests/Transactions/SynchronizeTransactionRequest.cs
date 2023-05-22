using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Requests.Transactions
{
    [Serializable]
    public class SynchronizeTransactionRequest : BaseServerRequest
    {
        public TransactionDto Transaction { get; set; }
    }
}
