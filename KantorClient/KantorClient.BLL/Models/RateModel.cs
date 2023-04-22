namespace KantorClient.BLL.Models
{
    public class RateModel
    {
        public long Id { get; set; }
        public CurrencyModel Currency { get; set; }
        public decimal DefaultRate { get; set; }
        public decimal MinimalRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Valid { get; set; }

        public RateModel()
        {
            
        }

        public RateModel(Model.Rate rate)
        {
            Id = rate.Id;
            DefaultRate = rate.DefaultRate;
            MinimalRate = rate.MinimalRate;
            StartDate = rate.StartDate;
            EndDate = rate.EndDate;
            Valid = rate.Valid;
            Currency = new CurrencyModel(rate.Currency);
        }
    }
}
