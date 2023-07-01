using KantorClient.BLL.Models;
using KantorClient.Common.Events;
using KantorClient.DAL.ResponseArgs;
using KantorClient.Model;

namespace KantorClient.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<LoginResponseArgs> LogIn(string username, string password, string kantorSymbol, bool offlineMode);
        public Task<bool> SetPln(decimal value);
        public Task<bool> AddPln(decimal value);

        public UserSession UserSession { get; }
        public void SetOnlineMode(bool mode);

        public event CashUpdated CashUpdated;
        public event OnlineModeChanged OnlineModeChanged;
    }
}
