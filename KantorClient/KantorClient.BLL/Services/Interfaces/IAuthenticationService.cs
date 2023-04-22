using KantorClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<bool> LogIn(string username, string password);

        public UserSession UserSession { get; }
    }
}
