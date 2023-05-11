using KantorServer.Model.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
