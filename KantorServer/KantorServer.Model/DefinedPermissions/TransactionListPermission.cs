using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class TransactionListPermission : Permission
    {
        public TransactionListPermission()
        {
            this.Key = PermissionKeys.Transaction.ListTransaction;
            this.Name = "Lista Transakcji";
            this.Description = "Pozwala wyświetlać listę transakcji";
            this.Module = PermissionKeys.Transaction.Module;
        }
    }
}
