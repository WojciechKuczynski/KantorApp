using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Model.Dtos
{
    [Serializable]
    public class CurrencyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public CurrencyDto()
        {
            
        }
        public CurrencyDto(Currency currency)
        {
            Id = currency.Id;
            Name = currency.Name;
            Symbol = currency.Symbol;
        }

        public static List<CurrencyDto> Map(IEnumerable<Currency> currencies) => currencies.Select(c => new CurrencyDto(c)).ToList();
        
        public Currency ConvertToEntity()
        {
            var currency = new Currency();
            if (Id > 0) 
                currency.Id = Id;
            currency.Name = Name;
            currency.Symbol = Symbol;
            return currency;
        }
    }
}
