using KantorServer.Model.Dtos;

namespace KantorServer.Application.Services.Interfaces
{
    public interface IUserPermissionService
    {
        Task<UserPermissionDto> CreateUserPermission(UserPermissionDto userPermissionDto);
        Task<UserPermissionDto> EditUserPermission(UserPermissionDto userPermissionDto);
        Task<bool> RemoveUserPermission(long userPermissionId);
        Task<UserPermissionDto> AssignPermissionsToUserPermission(UserPermissionDto userPermissionDto, List<PermissionDto> permissions);
        Task<List<PermissionDto>> GetPermissions();
        Task<List<UserPermissionDto>> GetUserPermissions();
        Task<bool> HasUserPermission(long UserId, IEnumerable<string> permissionKeys);
    }
}
