using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class TransferEditPermission : Permission
    {
        public TransferEditPermission()
        {
            this.Key = PermissionKeys.Transfer.EditTransfer;
            this.Name = "Edytowanie Transferów";
            this.Description = "Pozwala edytować transfery";
            this.Module = PermissionKeys.Transfer.Module;
        }
    }
}
