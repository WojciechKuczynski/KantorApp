using KantorServer.Model;

namespace KantorServer.Application.Services.Interfaces
{
    public interface ISessionService : IService
    {
        Task<UserSession> CheckSessionToken(string sessionToken);
    }
}
