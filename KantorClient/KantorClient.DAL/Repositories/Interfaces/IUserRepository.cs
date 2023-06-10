using KantorClient.Model;
using KantorServer.Model.Dtos;

namespace KantorClient.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserSession> UserLogin(string username, string password);
        public Task<List<UserDto>> GetUsers(string synchronizationKey);
        public Task<UserDto> EditUser(UserDto userDto, string synchronizationKey);
        public Task<UserDto> AddUser(UserDto userDto, string synchronizationKey);
        public Task<UserSession> SetPln(UserSession session, decimal value);
    }
}
