using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Responses.Currencies
{
    [Serializable]
    public class GetAllCurrenciesResponse : BaseServerResponse
    {
        public GetAllCurrenciesResponse(bool isCorrect, string? successMsg = null, string? failMsg = null) : base(isCorrect, successMsg, failMsg)
        {
        }

        public List<CurrencyDto> Currencies { get; set; }
    }
}
