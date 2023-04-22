using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Model
{
    public class Currency : BaseModel
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string ExternalId { get; set; }

        public Currency()
        {

        }
        public Currency(long id, string name, string symbol)
        {
            Id = id;
            Symbol = symbol;
            Name = name;
        }

        public Currency(KantorServer.Model.Dtos.CurrencyDto currency)
        {
            Name = currency.Name;
            Symbol = currency.Symbol;
            ExternalId = currency.Id.ToString();
        }
    }
}
