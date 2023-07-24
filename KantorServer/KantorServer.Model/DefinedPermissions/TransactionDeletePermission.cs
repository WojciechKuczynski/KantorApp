using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class TransactionDeletePermission : Permission
    {
        public TransactionDeletePermission()
        {
            this.Key = PermissionKeys.Transaction.DeleteTransaction;
            this.Name = "Usuwanie Transakcji";
            this.Description = "Pozwala usuwać transakcje";
            this.Module = PermissionKeys.Transaction.Module;
        }
    }
}
