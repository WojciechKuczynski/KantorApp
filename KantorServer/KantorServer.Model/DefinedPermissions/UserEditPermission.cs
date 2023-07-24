using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class UserEditPermission : Permission
    {
        public UserEditPermission()
        {
            this.Key = PermissionKeys.User.EditUser;
            this.Name = "Edytowanie Użytkowników";
            this.Description = "Pozwala edytować użytkowników";
            this.Module = PermissionKeys.User.Module;
        }
    }
}
