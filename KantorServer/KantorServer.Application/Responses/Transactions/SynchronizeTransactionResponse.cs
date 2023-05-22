using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Responses.Transactions
{
    [Serializable]
    public class SynchronizeTransactionResponse : BaseServerResponse
    {
        public SynchronizeTransactionResponse(bool isCorrect, string? successMsg = null, string? failMsg = null) : base(isCorrect, successMsg, failMsg)
        {
        }
        public SynchronizeTransactionResponse()
        {
            
        }
        public TransactionDto Transaction { get; set; }
    }
}
