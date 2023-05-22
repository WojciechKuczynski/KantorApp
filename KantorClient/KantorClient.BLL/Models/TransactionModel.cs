using KantorClient.Model;
using KantorClient.Model.Consts;

namespace KantorClient.BLL.Models
{
    public class TransactionModel
    {
        public long Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public CurrencyModel Currency { get; set; }
        public decimal Quantity { get; set; }
        public decimal FinalValue { get; set; }
        public decimal Rate { get; set; }
        public long UserId { get; set; }
        public long? Parent { get; set; }
        public long? ExternalId { get; set; }
        public bool Edited { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool Valid { get; set; }
        public DateTime? DeletionDate { get; set; }

        public TransactionModel(Transaction transaction)
        {
            Id = transaction.Id;
            TransactionType = transaction.TransactionType;
            Currency = new CurrencyModel(transaction.Currency);
            Quantity = transaction.Quantity;
            FinalValue = transaction.FinalValue;    
            Rate = transaction.Rate;
            Parent = transaction.Parent;
            ExternalId = transaction.ExternalId;
            UserId = transaction.User.UserId;
            TransactionDate = transaction.TransactionDate;
            Valid = transaction.Valid;  
            DeletionDate = transaction.DeletionDate;
            Edited = transaction.Edited;
        }

        public TransactionModel()
        {
            
        }

        public Transaction Map()
        {
            return new Transaction
            {
                Id = this.Id,
                TransactionType = this.TransactionType,
                Currency = this.Currency.Map(),
                Quantity = this.Quantity,
                FinalValue = this.FinalValue,
                Rate = this.Rate,
                Parent = this.Parent,
                ExternalId = this.ExternalId,
                TransactionDate = this.TransactionDate,
                Valid = this.Valid,
                DeletionDate = this.DeletionDate,
                Edited = this.Edited
            };
        }
    }
}
