using KantorClient.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.BLL.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<UserModel> AddUser(UserModel model);
        public Task<UserModel> EditUser(UserModel model);
        public Task<List<UserModel>> GetUsers();
    }
}
