using KantorClient.Model;

namespace KantorClient.BLL.Models
{
    public class CashRegistryModel
    {
        public long Id { get; set; }
        public CurrencyModel Currency { get; set; }
        public decimal Quantity { get; set; }
        public long KantorId { get; set; }

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
            KantorId = registry.KantorId;
        }

        public CashRegistry Map() => new CashRegistry()
        {
            Id = this.Id,
            Quantity = this.Quantity,
            Currency = this.Currency.Map(),
            KantorId = this.KantorId
        };

        public CashRegistryModel Clone() => new CashRegistryModel
        {
            Id = this.Id,
            Quantity = this.Quantity,
            Currency = this.Currency,
            KantorId = this.KantorId
        };
    }

}
