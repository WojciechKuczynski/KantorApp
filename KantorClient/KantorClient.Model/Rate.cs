namespace KantorClient.Model
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
        public long? ExternalId { get; set; }
        public bool Synchronized { get; set; }
        public bool UseNbpSpread { get; set; }
        public decimal Spread { get; set; }

        public Rate()
        {

        }
        public Rate(Currency currency, decimal defaultBuyRate, decimal maximumBuyRate, decimal defaultSellRate, decimal minimalSellRate, bool useNbpSpread, decimal spread, DateTime startDate, DateTime endDate, long externalId)
        {
            Currency = currency;
            DefaultBuyRate = defaultBuyRate;
            MaximumBuyRate = maximumBuyRate;
            DefaultSellRate = defaultSellRate;
            MinimalSellRate = minimalSellRate;
            Spread = spread;
            UseNbpSpread = useNbpSpread;
            StartDate = startDate;
            EndDate = endDate;
            Valid = true;
            ExternalId = externalId;
        }
        public Rate(KantorServer.Model.Dtos.RateDto rate)
        {
            Currency = new Currency(rate.Currency);
            DefaultBuyRate = rate.DefaultBuyRate;
            MaximumBuyRate = rate.MaximumBuyRate;
            DefaultSellRate = rate.DefaultSellRate;
            MinimalSellRate = rate.MinimalSellRate;
            StartDate = rate.StartDate;
            EndDate = rate.EndDate;
            Valid = rate.Valid;
            UseNbpSpread = rate.UseNbpSpread;
            Spread = rate.Spread;
            ExternalId = rate.Id;
        }
    }
}
