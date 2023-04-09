using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Requests
{
    public class LoginRequest
    {
        public UserDto User { get; set; }
        public KantorDto Kantor { get; set; }
    }
}
