using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class TransferAddPermission : Permission
    {
        public TransferAddPermission()
        {
            this.Key = PermissionKeys.Transfer.ListTransfer;
            this.Name = "Lista Transferów";
            this.Description = "Pozwala wyświetlać listę transferów";
            this.Module = PermissionKeys.Transfer.Module;
        }
    }
}
