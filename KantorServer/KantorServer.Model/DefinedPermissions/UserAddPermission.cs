using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class UserAddPermission : Permission
    {
        public UserAddPermission()
        {
            this.Key = PermissionKeys.User.AddUser;
            this.Name = "Dodawanie Użytkowników";
            this.Description = "Pozwala dodawać użytkowników";
            this.Module = PermissionKeys.User.Module;
        }
    }
}
