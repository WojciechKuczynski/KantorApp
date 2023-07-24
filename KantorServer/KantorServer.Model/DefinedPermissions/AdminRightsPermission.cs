using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class AdminRightsPermission : Permission
    {
        public AdminRightsPermission()
        {
            this.Key = PermissionKeys.Admin.AdminRights;
            this.Name = "Uprawnienia Admina";
            this.Description = "Obejście wszystkich uprawnień";
            this.Module = "Applikacja";
        }
    }
}
