using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Responses.Transfers
{
    [Serializable]
    public class SynchronizeTransferResponse : BaseServerResponse
    {
        public SynchronizeTransferResponse(bool isCorrect, string? successMsg = null, string? failMsg = null) : base(isCorrect, successMsg, failMsg)
        {
        }
        public SynchronizeTransferResponse()
        {
            
        }
        public TransferDto Transfer { get; set; }
    }
}
