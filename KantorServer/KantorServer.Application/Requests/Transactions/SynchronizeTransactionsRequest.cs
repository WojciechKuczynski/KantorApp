using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Requests.Transactions
{
    public class SynchronizeTransactionsRequest : BaseServerRequest
    {
        public List<TransactionDto> Transactions { get; set; }
    }
}
