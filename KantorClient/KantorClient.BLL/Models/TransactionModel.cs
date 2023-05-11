using KantorClient.Model;
using KantorClient.Model.Consts;

namespace KantorClient.BLL.Models
{
    public class TransactionModel
    {
        public TransactionType TransactionType { get; set; }
        public CurrencyModel Currency { get; set; }
        public decimal Quantity { get; set; }
        public decimal FinalValue { get; set; }
        public decimal Rate { get; set; }
        public long UserId { get; set; }
        public long? Parent { get; set; }
        public long? ExternalId { get; set; }

        public TransactionModel(Transaction transaction)
        {
            TransactionType = transaction.TransactionType;
            Currency = new CurrencyModel(transaction.Currency);
            Quantity = transaction.Quantity;
            FinalValue = transaction.FinalValue;    
            Rate = transaction.Rate;
            Parent = transaction.Parent;
            ExternalId = transaction.ExternalId;
            UserId = transaction.User.UserId;
        }
    }
}
