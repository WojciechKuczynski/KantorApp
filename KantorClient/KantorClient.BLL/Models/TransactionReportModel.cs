using KantorClient.Model.Consts;
using KantorServer.Model.Dtos;

namespace KantorClient.BLL.Models
{
    public class TransactionReportModel
    {
        public long Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public CurrencyModel Currency { get; set; }
        public decimal Quantity { get; set; }
        public decimal FinalValue { get; set; }
        public decimal Rate { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string KantorName { get; set; }
        public long? Parent { get; set; }
        public long? ExternalId { get; set; }
        public bool Edited { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool Valid { get; set; }
        public DateTime? DeletionDate { get; set; }

        public TransactionReportModel(TransactionDto transaction)
        {
            TransactionType = (TransactionType)transaction.TransactionType;
            Currency = new CurrencyModel(new Model.Currency(transaction.Currency));
            Quantity = transaction.Quantity;
            FinalValue = transaction.FinalValue;
            Rate = transaction.Rate;
            UserId = transaction.User.Id;
            UserName = transaction.User.Name;
            KantorName = transaction.Kantor.Name;
            ExternalId = transaction.Id;
            TransactionDate = transaction.TransactionDate;
            Edited = transaction.Edited;
            Valid = transaction.Valid;
            DeletionDate = transaction.DeletionDate;
            Parent = transaction.Parent;
        }
    }
}
