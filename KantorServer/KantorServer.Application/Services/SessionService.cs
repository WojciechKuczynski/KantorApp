using KantorServer.Application.Services.Interfaces;
using KantorServer.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Services
{
    public class SessionService : ISessionService
    {
        public DataContext DataContext { get; }

        public SessionService(DataContext dataContext)
        {
            DataContext = dataContext;
        }
        public async Task<bool> CheckSessionToken(string sessionToken)
        {
            var session = await DataContext.UserSessions.FirstOrDefaultAsync(x => x.SynchronizationKey == sessionToken);
            return session != null;
        }
    }
}
