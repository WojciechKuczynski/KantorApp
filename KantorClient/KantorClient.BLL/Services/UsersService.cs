using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.Repositories.Interfaces;
using KantorServer.Model.Dtos;
using System.Security.Cryptography;
using System.Text;

namespace KantorClient.BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;

        public UsersService(IAuthenticationService authenticationService, IUserRepository userRepository)
        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }

        public async Task<UserModel> AddUser(UserModel model)
        {
            var dto = new UserDto
            {
                Login = model.Login,
                Name = model.Name,
                Password = CreateMD5(model.Password),
                Permission = (KantorServer.Model.Consts.UserPermission)model.Permission,
                Valid = true
            };
            var addedUser = await _userRepository.AddUser(dto, _authenticationService.UserSession.SynchronizationKey);

            return new UserModel(addedUser);
        }

        public async Task<UserModel> EditUser(UserModel model)
        {
            var dto = new UserDto
            {
                Id = model.Id,
                Login = model.Login,
                Name = model.Name,
                Password = CreateMD5(model.Password),
                Permission = (KantorServer.Model.Consts.UserPermission)model.Permission,
                Valid = model.Valid,
            };
            var addedUser = await _userRepository.EditUser(dto, _authenticationService.UserSession.SynchronizationKey);

            return new UserModel(addedUser);
        }

        public async Task<List<UserModel>> GetUsers()
        {
            var users = await _userRepository.GetUsers(_authenticationService.UserSession.SynchronizationKey);

            return users.Select(x => new UserModel(x)).ToList();
        }

        private string CreateMD5(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }
    }
}
