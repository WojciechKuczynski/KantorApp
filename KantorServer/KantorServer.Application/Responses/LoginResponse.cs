using KantorServer.Model.Consts;
using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Responses
{
    [Serializable]
    public class LoginResponse : BaseServerResponse
    {
        public string SynchronizationKey { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public UserPermission Permission { get; set; }
        public KantorDto Kantor { get; set; }
        public LoginResponse() : base(false) { }
        public LoginResponse(bool isCorrect, string successMsg, string failMsg) : base(isCorrect, successMsg, failMsg)
        {
        }
    }
}
