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

        public Transaction()
        {

        }

        public Transaction(TransactionType transactionType, Currency currency, decimal quantity, decimal finalValue, decimal rate, UserSession user)
        {
            TransactionType = transactionType;
            Currency = currency;
            Quantity = quantity;
            FinalValue = finalValue;
            Rate = rate;
            User = user;
        }
    }
}
