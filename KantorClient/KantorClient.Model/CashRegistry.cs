namespace KantorClient.Model
{
    public class CashRegistry : BaseModel
    {
        public virtual Currency Currency { get; set; }
        public decimal Quantity { get; set; }
        public long KantorId { get; set; }

        public CashRegistry()
        {

        }
    }
}
