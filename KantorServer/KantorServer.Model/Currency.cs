using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Model
{
    public class Currency : BaseModel
    {
        public string Name { get; set; }
        public string Symbol { get; set; }

        public Currency()
        {
            
        }
        public Currency(string name, string symbol)
        {
            Symbol = symbol;
            Name = name;
        }
    }
}
