using KantorServer.Model.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace KantorServer.Model.DefinedPermissions
{
    [Table("Permissions")]
    public class TransactionAddPermission : Permission
    {
        public TransactionAddPermission()
        {
            this.Key = PermissionKeys.Transaction.AddTransaction;
            this.Name = "Dodawanie Transakcji";
            this.Description = "Pozwala dodawać transakcje";
            this.Module = PermissionKeys.Transaction.Module;
        }
    }
}
