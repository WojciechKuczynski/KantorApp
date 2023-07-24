using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class TransferDeletePermission : Permission
    {
        public TransferDeletePermission()
        {
            this.Key = PermissionKeys.Transfer.DeleteTransfer;
            this.Name = "Usuwanie Transferów";
            this.Description = "Pozwala usuwać transfery";
            this.Module = PermissionKeys.Transfer.Module;
        }
    }
}
