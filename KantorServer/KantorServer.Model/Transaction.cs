using KantorServer.Model.Consts;

namespace KantorServer.Model
{
    public class Transaction : BaseModel
    {
        public TransactionType TransactionType { get; set; }
        public virtual Currency Currency { get; set; }
        public decimal Quantity { get; set; }
        public decimal FinalValue { get; set; }
        public decimal Rate { get; set; }
        public virtual Kantor Kantor { get; set; }
        public virtual User User { get; set; }
        public long? Parent { get; set; }
        public long ExternalId { get; set; }
        public bool Edited { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool Valid { get; set; }
        public DateTime? DeletionDate { get; set; }


        public Transaction()
        {

        }
        public Transaction(TransactionType transactionType, Currency currency, decimal quantity, decimal finalValue, decimal rate, Kantor kantor, User user)
        {
            TransactionType = transactionType;
            Currency = currency;
            Quantity = quantity;
            FinalValue = finalValue;
            Rate = rate;
            Kantor = kantor;
            User = user;
            TransactionDate = DateTime.Now;
            Valid = true;
        }
    }
}
