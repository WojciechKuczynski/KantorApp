using KantorServer.Application.Services.Interfaces;
using KantorServer.DAL;
using KantorServer.Model;
using Microsoft.EntityFrameworkCore;

namespace KantorServer.Application.Services
{
    public class SessionService : ISessionService
    {
        public DataContext DataContext { get; }

        public SessionService(DataContext dataContext)
        {
            DataContext = dataContext;
        }
        public async Task<UserSession> CheckSessionToken(string sessionToken)
        {
            var session = await DataContext.UserSessions.Include(x => x.User).FirstOrDefaultAsync(x => x.SynchronizationKey == sessionToken);
            return session;
        }
    }
}
