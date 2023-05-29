using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Requests.Transfers
{
    [Serializable]
    public class SynchronizeTransferRequest : BaseServerRequest
    {
        public TransferDto Transfer { get; set; }
    }
}
