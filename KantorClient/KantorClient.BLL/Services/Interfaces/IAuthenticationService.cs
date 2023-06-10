using KantorClient.Model;

namespace KantorClient.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<bool> LogIn(string username, string password);
        public Task<bool> SetPln(decimal value);

        public UserSession UserSession { get; }
    }
}
