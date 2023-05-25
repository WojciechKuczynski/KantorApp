using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Responses.Users
{
    [Serializable]
    public class GetAllUsersResponse : BaseServerResponse
    {
        public GetAllUsersResponse(bool isCorrect, string? successMsg = null, string? failMsg = null) : base(isCorrect, successMsg, failMsg)
        {
        }

        public GetAllUsersResponse()
        {
            
        }
        public List<UserDto> Users { get; set; }
    }
}
