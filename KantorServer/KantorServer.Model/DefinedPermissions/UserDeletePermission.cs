using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class UserDeletePermission : Permission
    {
        public UserDeletePermission()
        {
            this.Key = PermissionKeys.User.DeleteUser;
            this.Name = "Usuwanie użytkowników";
            this.Description = "Pozwala usuwać użytkowników";
            this.Module = PermissionKeys.User.Module;
        }
    }
}
