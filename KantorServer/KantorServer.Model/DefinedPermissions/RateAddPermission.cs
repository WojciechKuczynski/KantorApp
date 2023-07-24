using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class RateAddPermission : Permission
    {
        public RateAddPermission()
        {
            this.Key = PermissionKeys.Rate.AddRate;
            this.Name = "Dodawanie Kursów";
            this.Description = "Pozwala dodawać kursy";
            this.Module = PermissionKeys.Rate.Module;
        }
    }
}
