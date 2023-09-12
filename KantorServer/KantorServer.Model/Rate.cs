namespace KantorServer.Model
{
    public class Rate : BaseModel
    {
        public virtual Currency Currency { get; set; }
        public decimal DefaultBuyRate { get; set; }
        public decimal MaximumBuyRate { get; set; }
        public decimal DefaultSellRate { get; set; }
        public decimal MinimalSellRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Valid { get; set; }
        public long ExternalId { get; set; }
        public bool UseNbpSpread { get; set; }
        public decimal Spread { get; set; }

        public Rate()
        {

        }
        public Rate(Currency currency)
        {
            Currency = currency;
        }
    }
}
