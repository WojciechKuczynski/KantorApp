using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Responses.Transactions
{
    public class SynchronizeTransactionsResponse : BaseServerResponse
    {
        public SynchronizeTransactionsResponse(bool isCorrect, string? successMsg = null, string? failMsg = null) : base(isCorrect, successMsg, failMsg)
        {
        }

        public List<TransactionDto> Transactions { get; set; }

    }
}
