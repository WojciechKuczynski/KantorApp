using KantorClient.BLL.Services.Interfaces;
using KantorClient.Common.Events;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;

namespace KantorClient.BLL.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public event CashUpdated CashUpdated;

        public UserSession UserSession { get; private set; }

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> LogIn(string username, string password)
        {
            UserSession = await _userRepository.UserLogin(username, password);
            return UserSession != null;
        }

        public async Task<bool> SetPln(decimal value)
        {
            var session = await _userRepository.SetPln(UserSession, value);
            UserSession.Cash = session.Cash;
            CashUpdated?.Invoke(this, value);
            return UserSession != null;
        }

        public async Task<bool> AddPln(decimal value)
        {
            return await SetPln(UserSession.Cash + value);
        }
    }
}
