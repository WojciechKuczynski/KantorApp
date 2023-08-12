namespace KantorServer.Model.Consts
{
    public static class PermissionKeys
    {
        public static class User
        {
            public const string Module = "Użytkownicy";
            public const string AddUser = $"{nameof(User)}_{nameof(AddUser)}";
            public const string EditUser = $"{nameof(User)}_{nameof(EditUser)}";
            public const string DeleteUser = $"{nameof(User)}_{nameof(DeleteUser)}";
            public const string ListUser = $"{nameof(User)}_{nameof(ListUser)}";
        }

        public static class Permission
        {
            public const string Module = "Uprawnienia";
            public const string AddPermission = $"{nameof(Permission)}_{nameof(AddPermission)}";
            public const string EditPermission = $"{nameof(Permission)}_{nameof(EditPermission)}";
            public const string DeletePermission = $"{nameof(Permission)}_{nameof(DeletePermission)}";
            public const string ListPermission = $"{nameof(Permission)}_{nameof(ListPermission)}";
        }

        public static class Transfer
        {
            public const string Module = "Transfery";
            public const string AddTransfer = $"{nameof(Transfer)}_{nameof(AddTransfer)}";
            public const string EditTransfer = $"{nameof(Transfer)}_{nameof(EditTransfer)}";
            public const string DeleteTransfer = $"{nameof(Transfer)}_{nameof(DeleteTransfer)}";
            public const string ListTransfer = $"{nameof(Transfer)}_{nameof(ListTransfer)}";
        }

        public static class Transaction
        {
            public const string Module = "Transakcje";
            public const string AddTransaction = $"{nameof(Transaction)}_{nameof(AddTransaction)}";
            public const string EditTransaction = $"{nameof(Transaction)}_{nameof(EditTransaction)}";
            public const string DeleteTransaction = $"{nameof(Transaction)}_{nameof(DeleteTransaction)}";
            public const string ListTransaction = $"{nameof(Transaction)}_{nameof(ListTransaction)}";
        }

        public static class Rate
        {
            public const string Module = "Kursy";
            public const string AddRate = $"{nameof(Rate)}_{nameof(AddRate)}";
            public const string EditRate = $"{nameof(Rate)}_{nameof(EditRate)}";
            public const string DeleteRate = $"{nameof(Rate)}_{nameof(DeleteRate)}";
            public const string ListRate = $"{nameof(Rate)}_{nameof(ListRate)}";
        }

        public static class Report
        {
            public const string Module = "Raporty";
            public const string ListReport = $"{nameof(Report)}_{nameof(ListReport)}";
        }

        public static class Admin
        {
            public const string Module = "Admin";
            public const string AdminRights = $"{nameof(Admin)}_{nameof(AdminRights)}";
        }
    }
}
