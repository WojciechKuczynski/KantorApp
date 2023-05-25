using KantorClient.Model;
using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserSession> UserLogin (string username, string password);
        public Task<List<UserDto>> GetUsers (string synchronizationKey);
        public Task<UserDto> EditUser (UserDto userDto, string synchronizationKey);
        public Task<UserDto> AddUser (UserDto userDto, string synchronizationKey);
    }
}
