using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Responses.Transfers
{
    public class GetAllTransfersResponse : BaseServerResponse
    {
        public List<TransferDto> Transfers { get; set; }
        public GetAllTransfersResponse(bool isCorrect, string successMsg, string failMsg) : base(isCorrect, successMsg, failMsg)
        {
        }
    }
}
