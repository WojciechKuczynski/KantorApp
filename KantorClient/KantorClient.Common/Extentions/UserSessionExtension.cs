using KantorClient.Model;
using KantorServer.Model.Consts;

namespace KantorClient.Common.Extentions
{
    public static class UserSessionExtension
    {
        public static bool HasUserPermission(this UserSession userSession, string permission)
        {
            return userSession.UserPermission.Contains(PermissionKeys.Admin.AdminRights) || userSession.UserPermission.Contains(permission);
        }
    }
}
