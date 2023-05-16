using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Responses.Rates
{
    [Serializable]
    public class AddEditRateResponse : BaseServerResponse
    {
        public AddEditRateResponse(RateDto rate, string? successMsg = null, string? failMsg = null) : base(rate != null, successMsg, failMsg)
        {
        }
        public AddEditRateResponse()
        {
            
        }
        public RateDto Rate { get; set; }
    }
}
