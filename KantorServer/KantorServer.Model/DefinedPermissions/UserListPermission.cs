using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class UserListPermission : Permission
    {
        public UserListPermission()
        {
            this.Key = PermissionKeys.User.ListUser;
            this.Name = "Lista Użytkowników";
            this.Description = "Pozwala wyświetlać listę użytkowników";
            this.Module = PermissionKeys.User.Module;
        }
    }
}
