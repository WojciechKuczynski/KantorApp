using KantorServer.Model.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Responses
{
    [Serializable]
    public class BaseServerResponse
    {
        public BaseServerResponse() : this(false) { }
        public BaseServerResponse(bool isCorrect, string? successMsg = null, string? failMsg = null)
        {
            ResponseType = isCorrect == true ? ServerResponseType.Success : ServerResponseType.Error;
            ResponseText = (isCorrect == true ? successMsg : failMsg) ?? string.Empty;
        }

        public ServerResponseType ResponseType { get; set; }
        public string ResponseText { get; set; }
    }
}
