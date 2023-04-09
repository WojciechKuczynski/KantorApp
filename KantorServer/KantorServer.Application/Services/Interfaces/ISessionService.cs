using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Services.Interfaces
{
    public interface ISessionService : IService
    {
        Task<bool> CheckSessionToken(string sessionToken); 
    }
}
