using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class RateEditPermission : Permission
    {
        public RateEditPermission()
        {
            this.Key = PermissionKeys.Rate.EditRate;
            this.Name = "Edytowanie Kursów";
            this.Description = "Pozwala edytować kursy";
            this.Module = PermissionKeys.Rate.Module;
        }
    }
}
