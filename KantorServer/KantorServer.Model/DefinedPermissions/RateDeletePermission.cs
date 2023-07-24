using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class RateDeletePermission : Permission
    {
        public RateDeletePermission()
        {
            this.Key = PermissionKeys.Rate.DeleteRate;
            this.Name = "Usuwanie Kursów";
            this.Description = "Pozwala usuwać kursy";
            this.Module = PermissionKeys.Rate.Module;
        }
    }
}
