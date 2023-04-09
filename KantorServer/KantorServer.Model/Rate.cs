using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Model
{
    public class Rate : BaseModel
    {
        public virtual Currency Currency { get; set; }
        public decimal DefaultRate { get; set; }
        public decimal MinimalRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Valid { get; set; }

        public Rate()
        {
            
        }
        public Rate(Currency currency)
        {
            Currency = currency;
        }
    }
}
