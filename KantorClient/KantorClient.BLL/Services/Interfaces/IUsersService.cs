using KantorClient.BLL.Models;

namespace KantorClient.BLL.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<UserModel> AddUser(UserModel model);
        Task<UserPermissionModel> AddUserPermission(UserPermissionModel userPermission);
        public Task<UserModel> EditUser(UserModel model);
        Task<UserPermissionModel> EditUserPermission(UserPermissionModel userPermission);
        Task<IEnumerable<PermissionModel>> GetPermissions();
        Task<List<UserPermissionModel>> GetUserPermissions();
        public Task<List<UserModel>> GetUsers();

        Task<bool> SavePermissionsToUserPermission(UserPermissionModel userPermission, List<PermissionModel> permissions);
    }
}
