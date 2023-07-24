using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class ReportListPermission : Permission
    {
        public ReportListPermission()
        {
            this.Key = PermissionKeys.Report.ListReport;
            this.Name = "Lista Raportów";
            this.Description = "Pozwala wyświetlać listę raportów";
            this.Module = PermissionKeys.Report.Module;
        }
    }
}
