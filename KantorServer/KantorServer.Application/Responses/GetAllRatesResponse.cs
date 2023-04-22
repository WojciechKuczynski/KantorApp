using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Responses
{
    [Serializable]
    public class GetAllRatesResponse : BaseServerResponse
    {
        public List<RateDto> Rates { get; set; }
        public GetAllRatesResponse() : base(false) { }    
        public GetAllRatesResponse(bool isCorrect, string? successMsg = null, string? failMsg = null) : base(isCorrect, successMsg, failMsg)
        {
        }
    }
}
