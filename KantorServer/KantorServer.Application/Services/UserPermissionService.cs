using KantorServer.Application.Services.Interfaces;
using KantorServer.DAL;
using KantorServer.Model.Consts;
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
    }
}
