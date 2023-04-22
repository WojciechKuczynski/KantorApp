using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;

namespace KantorClient.BLL.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        public UserSession UserSession { get; private set; }

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> LogIn(string username, string password)
        {
            // TODO: dodanie do lokalnej bazy
            UserSession = await _userRepository.UserLogin(username, password);
            return UserSession != null;
        }
    }
}
