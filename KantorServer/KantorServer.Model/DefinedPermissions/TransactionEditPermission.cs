using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class TransactionEditPermission : Permission
    {
        public TransactionEditPermission()
        {
            this.Key = PermissionKeys.Transaction.EditTransaction;
            this.Name = "Edytowanie Transakcji";
            this.Description = "Pozwala edytować transakcje";
            this.Module = PermissionKeys.Transaction.Module;
        }
    }
}
