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
        public decimal TransferValue { get; set; }
        public virtual Currency TransferCurrency { get; set; }

        public Transfer()
        {
            
        }
        public Transfer(Kantor kantor)
        {
            Kantor = kantor;
        }
    }
}
