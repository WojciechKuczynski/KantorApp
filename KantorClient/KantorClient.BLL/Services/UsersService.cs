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
                Permission = new UserPermissionDto { Id = model.UserPermissionId },
                Valid = true
            };
            var addedUser = await _userRepository.AddUser(dto, _authenticationService.UserSession.SynchronizationKey);

            return new UserModel(addedUser);
        }

        public async Task<UserPermissionModel> AddUserPermission(UserPermissionModel userPermission)
        {
            var dto = userPermission.ToDto();
            var addedPermissionDto = await _userRepository.AddEditUserPermission(dto, _authenticationService.UserSession.SynchronizationKey);

            if (addedPermissionDto == null)
            {
                return null;
            }

            return new UserPermissionModel(addedPermissionDto);
        }

        public async Task<UserModel> EditUser(UserModel model)
        {
            var dto = new UserDto
            {
                Id = model.Id,
                Login = model.Login,
                Name = model.Name,
                Password = CreateMD5(model.Password),
                Permission = new UserPermissionDto { Id = model.UserPermissionId },
                Valid = model.Valid,
            };
            var addedUser = await _userRepository.EditUser(dto, _authenticationService.UserSession.SynchronizationKey);
            if (addedUser == null)
                return null;
            return new UserModel(addedUser);
        }

        public async Task<UserPermissionModel> EditUserPermission(UserPermissionModel userPermission)
        {
            var dto = userPermission.ToDto();
            var editedPermissionDto = await _userRepository.AddEditUserPermission(dto, _authenticationService.UserSession.SynchronizationKey);

            if (editedPermissionDto == null)
            {
                return null;
            }

            return new UserPermissionModel(editedPermissionDto);
        }

        public async Task<IEnumerable<PermissionModel>> GetPermissions()
        {
            var permissions = await _userRepository.GetPermissions(_authenticationService.UserSession.SynchronizationKey);
            if (permissions == null)
            {
                return new List<PermissionModel>();
            }

            return permissions.Select(x => new PermissionModel(x)).ToList();
        }

        public async Task<List<UserPermissionModel>> GetUserPermissions()
        {
            var userPermissions = await _userRepository.GetUserPermissions(_authenticationService.UserSession.SynchronizationKey);
            if (userPermissions == null)
            {
                return new List<UserPermissionModel>();
            }

            return userPermissions.Select(x => new UserPermissionModel(x)).ToList();
        }

        public async Task<List<UserModel>> GetUsers()
        {
            var users = await _userRepository.GetUsers(_authenticationService.UserSession.SynchronizationKey);
            if (users == null)
            {
                return new List<UserModel>();
            }

            return users.Select(x => new UserModel(x)).ToList();
        }

        public Task<bool> SavePermissionsToUserPermission(UserPermissionModel userPermission, List<PermissionModel> permissions)
        {
            var userPerm = userPermission.ToDto();
            var perms = permissions.Select(x => x.ToDto()).ToList();
            return _userRepository.SavePermissionsToUserPermission(userPerm, perms, _authenticationService.UserSession.SynchronizationKey);

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
