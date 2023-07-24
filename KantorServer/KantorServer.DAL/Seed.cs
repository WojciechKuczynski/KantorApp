using KantorServer.Model;
using KantorServer.Model.DefinedPermissions;
using Microsoft.EntityFrameworkCore;

namespace KantorServer.DAL
{
    public class Seed
    {
        public static async Task SeedDataAsync(DataContext context)
        {
            await SeedPermissionsAsync(context);
            await context.SaveChangesAsync();
        }

        private static async Task SeedPermissionsAsync(DataContext context)
        {
            var definePermissions = new AdminRightsPermission().GetType().Assembly.GetTypes()
                .Where(x => x.BaseType == typeof(Permission))
                .ToList();
            var permissions = context.Permissions.AsQueryable();

            foreach (var permission in definePermissions)
            {
                var perm = Activator.CreateInstance(permission) as Permission;
                if (await permissions.FirstOrDefaultAsync(x => x.Key == perm.Key) != null)
                    continue;

                await context.Permissions.AddAsync(perm);
            }
        }
    }
}
