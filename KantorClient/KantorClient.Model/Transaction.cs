using KantorClient.Model.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Model
{
    public class Transaction : BaseModel
    {
        public TransactionType TransactionType { get; set; }
        public virtual Currency Currency { get; set; }
        public decimal Quantity { get; set; }
        public decimal FinalValue { get; set; }
        public decimal Rate { get; set; }
        public virtual UserSession User { get; set; }
        public long? Parent { get; set; }
        public long? ExternalId { get; set; }
        public bool Synchronized { get; set; }
        public bool Edited { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool Valid { get; set; }
        public DateTime? DeletionDate { get; set; }

        public Transaction()
        {

        }

        public Transaction(TransactionType transactionType, Currency currency, decimal quantity, decimal finalValue, decimal rate, UserSession user, long? parent)
        {
            TransactionType = transactionType;
            Currency = currency;
            Quantity = quantity;
            FinalValue = finalValue;
            Rate = rate;
            User = user;
            Parent = parent;
        }
    }
}
