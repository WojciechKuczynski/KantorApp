using KantorClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.BLL.Models
{
    public class CurrencyModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public CurrencyModel()
        {
            
        }
        public CurrencyModel(Model.Currency currency)
        {
            Id = currency.Id;
            Name = currency.Name;
            Symbol = currency.Symbol;
        }

        public static Currency Map(CurrencyModel model) => new Currency
        {
            Id = model.Id,
            Name = model.Name,
            Symbol = model.Symbol,
            ExternalId = model.Id.ToString()
        };

        public override string ToString()
        {
            return $"{Name} ({Symbol})";
        }
    }
}
