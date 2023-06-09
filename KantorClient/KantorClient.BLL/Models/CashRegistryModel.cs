using KantorClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.BLL.Models
{
    public class CashRegistryModel
    {
        public long Id { get; set; }
        public CurrencyModel Currency { get; set; }
        public decimal Quantity { get; set; }

        public CashRegistryModel()
        {
            
        }

        public CashRegistryModel(CashRegistry registry)
        {
            Id = registry.Id;
            if (registry.Currency != null)
            {
                Currency = new CurrencyModel(registry.Currency);
            }

            Quantity = registry.Quantity;
        }

        public CashRegistry Map() => new CashRegistry()
        {
            Id = this.Id,
            Quantity = this.Quantity,
            Currency = this.Currency.Map()
        };
    }

}
