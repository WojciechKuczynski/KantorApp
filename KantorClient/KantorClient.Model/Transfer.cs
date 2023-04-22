using KantorClient.Model.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Model
{
    public class Transfer : BaseModel
    {
        public TransferType Type { get; set; }
        public decimal TransferValue { get; set; }
        public virtual Currency TransferCurrency { get; set; }
    }
}
