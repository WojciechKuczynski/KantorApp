using KantorClient.Model;

namespace KantorClient.Common.Extentions
{
    public static class UserSessionExtension
    {
        public static bool HasUserPermission(this UserSession userSession, string permission)
        {
            return userSession.UserPermission.Contains(permission);
        }
    }
}
