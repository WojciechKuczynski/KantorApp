namespace KantorClient.Model
{
    public class Rate : BaseModel
    {
        public virtual Currency Currency { get; set; }
        public decimal DefaultBuyRate { get; set; }
        public decimal MinimalBuyRate { get; set; }
        public decimal DefaultSellRate { get; set; }
        public decimal MinimalSellRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Valid { get; set; }
        public long? ExternalId { get; set; }

        public Rate()
        {

        }
        public Rate(Currency currency, decimal defaultBuyRate, decimal minimalBuyRate, decimal defaultSellRate, decimal minimalSellRate, DateTime startDate, DateTime endDate, long externalId)
        {
            Currency = currency;
            DefaultBuyRate = defaultBuyRate;
            MinimalBuyRate = minimalBuyRate;
            DefaultSellRate = defaultSellRate;
            MinimalSellRate = minimalSellRate;
            StartDate = startDate;
            EndDate = endDate;
            Valid = true;
            ExternalId = externalId;
        }
        public Rate(KantorServer.Model.Dtos.RateDto rate)
        {
            Currency = new Currency(rate.Currency);
            DefaultBuyRate = rate.DefaultBuyRate;
            MinimalBuyRate = rate.MinimalBuyRate;
            DefaultSellRate = rate.DefaultSellRate;
            MinimalSellRate = rate.MinimalSellRate;
            StartDate = rate.StartDate;
            EndDate = rate.EndDate;
            Valid = true;
            ExternalId = rate.Id;
        }
    }
}
