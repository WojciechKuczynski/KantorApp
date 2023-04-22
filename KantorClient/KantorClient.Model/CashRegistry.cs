using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Model
{
    public class CashRegistry : BaseModel
    {
        public virtual Currency Currency { get; set; }
        public decimal Quantity { get; set; }

        public CashRegistry()
        {
            
        }
    }
}
