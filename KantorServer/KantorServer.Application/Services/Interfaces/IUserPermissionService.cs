namespace KantorServer.Application.Services.Interfaces
{
    public interface IUserPermissionService
    {
        Task<bool> HasUserPermission(long UserId, IEnumerable<string> permissionKeys);
    }
}
