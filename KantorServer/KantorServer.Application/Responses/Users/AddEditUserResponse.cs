using KantorServer.Model;
using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Responses.Users
{
    [Serializable]
    public class AddEditUserResponse : BaseServerResponse
    {
        public UserDto User { get; set; }
        public AddEditUserResponse(UserDto user, string? successMsg = null, string? failMsg = null) : base(user != null, successMsg, failMsg)
        {
            User = user;
        }
        public AddEditUserResponse()
        {
            
        }
    }
}
