namespace KantorClient.Model
{
    public class Rate : BaseModel
    {
        public virtual Currency Currency { get; set; }
        public decimal DefaultRate { get; set; }
        public decimal MinimalRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Valid { get; set; }
        public string ExternalId { get; set; }

        public Rate()
        {

        }
        public Rate(Currency currency, decimal defaultRate, decimal minimalRate, DateTime startDate, DateTime endDate, string externalId)
        {
            Currency = currency;
            DefaultRate = defaultRate;
            MinimalRate = minimalRate;
            StartDate = startDate;
            EndDate = endDate;
            Valid = true;
            ExternalId = externalId;
        }
        public Rate(KantorServer.Model.Dtos.RateDto rate)
        {
            Currency = new Currency(rate.Currency);
            DefaultRate = rate.DefaultRate;
            MinimalRate = rate.MinimalRate;
            StartDate = rate.StartDate;
            EndDate = rate.EndDate;
            Valid = true;
            ExternalId = rate.Id.ToString();
        }
    }
}
