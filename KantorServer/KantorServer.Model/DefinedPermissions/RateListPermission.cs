using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class RateListPermission : Permission
    {
        public RateListPermission()
        {
            this.Key = PermissionKeys.Rate.ListRate;
            this.Name = "Wyświetlanie Kursów";
            this.Description = "Pozwala wyświetlać listę kursów";
            this.Module = PermissionKeys.Rate.Module;
        }
    }
}
