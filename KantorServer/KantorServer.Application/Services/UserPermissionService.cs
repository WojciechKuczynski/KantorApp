using KantorServer.Application.Services.Interfaces;
using KantorServer.DAL;
using KantorServer.Model.Consts;
using KantorServer.Model.Dtos;
using Microsoft.EntityFrameworkCore;

namespace KantorServer.Application.Services
{
    public class UserPermissionService : IUserPermissionService
    {
        private DataContext DataContext { get; }
        public UserPermissionService(DataContext dataContext)
        {
             this.DataContext = dataContext;
        }
        public async Task<bool> HasUserPermission(long UserId, IEnumerable<string> permissionKeys)
        {
            try
            {
                var user = await DataContext.Users.Include(x => x.Permission).ThenInclude(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == UserId);
                if (user == null)
                {
                    return false;
                }

                return permissionKeys.All(p => user.Permission.Permissions.Any(x => x.Key == p || x.Key == PermissionKeys.Admin.AdminRights));
            }
            catch(Exception ex)
            {

            }
            return false;
        }

        public async Task<List<UserPermissionDto>> GetUserPermissions()
        {
            try
            {
                var userPermissions = await DataContext.UserPermissions.Include(x => x.Permissions).Include(x => x.Permissions).ToListAsync();
                if (userPermissions == null)
                {
                    return new List<UserPermissionDto>();
                }

                return userPermissions.Select(x => new UserPermissionDto(x)).ToList();
            }
            catch(Exception ex)
            {

            }
            return new List<UserPermissionDto>();
        }

        public async Task<List<PermissionDto>> GetPermissions()
        {
            try
            {
                var permissions = await DataContext.Permissions.ToListAsync();
                if (permissions == null)
                {
                    return new List<PermissionDto>();
                }

                return permissions.Select(x => new PermissionDto(x)).ToList();
            }
            catch(Exception ex)
            {

            }
            return new List<PermissionDto>();
        }

        public async Task<UserPermissionDto> CreateUserPermission(UserPermissionDto userPermissionDto)
        {
            if (string.IsNullOrEmpty(userPermissionDto.Name))
            {
                throw new Exception("Wymagana nazwa uprawnienia");
            }

            var userInDb = await DataContext.UserPermissions.AddAsync(userPermissionDto.ConvertToEntity());
            await DataContext.SaveChangesAsync();
            if (userInDb.State == EntityState.Added)
            {
                return new UserPermissionDto(userInDb.Entity);
            }

            return userPermissionDto;
        }

        public async Task<UserPermissionDto> EditUserPermission(UserPermissionDto userPermissionDto)
        {
            var permissionInDb = await DataContext.UserPermissions.Include(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == userPermissionDto.Id);
            if (permissionInDb == null)
            {
                throw new Exception("Nie znaleziono uprawnienia");
            }

            foreach( var permission in permissionInDb.Permissions)
            {
                if (!userPermissionDto.Permissions.Any(x => x.Id == permission.Id))
                {
                    permissionInDb.Permissions.Remove(permission);
                }
            }

            foreach (var permission in userPermissionDto.Permissions)
            {
                if (!permissionInDb.Permissions.Any(x => x.Id == permission.Id))
                {
                    var permInDb = await DataContext.Permissions.FirstOrDefaultAsync(x => x.Id == permission.Id);
                    if (permInDb != null)
                    {
                        permissionInDb.Permissions.Add(permInDb);
                    }
                }
            }
            await DataContext.SaveChangesAsync();
            return new UserPermissionDto(permissionInDb);
        }

        public Task<bool> RemoveUserPermission(long userPermissionId)
        {
            // for now we don't remove user permissions
            return Task.FromResult(false);
        }

        public Task<UserPermissionDto> AssignPermissionsToUserPermission(UserPermissionDto userPermissionDto, List<PermissionDto> permissions)
        {
            // Do it in EditUserPermission ?
            return null;
        }
    }
}