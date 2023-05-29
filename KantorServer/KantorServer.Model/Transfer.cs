using KantorServer.Model.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KantorServer.Model
{
    public class Transfer : BaseModel
    {
        public TransferType TransferType { get; set; }
        public virtual Kantor Kantor { get; set; }
        public virtual User User { get; set; }
        public decimal TransferValue { get; set; }
        public virtual Currency TransferCurrency { get; set; }
        public long ExternalId { get; set; }
        public bool Valid { get; set; }
        public long? Parent { get; set; }
        public bool Edited { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime? DeletionDate { get; set; }

        public Transfer()
        {
            
        }
        public Transfer(Kantor kantor)
        {
            Kantor = kantor;
        }
    }
}
