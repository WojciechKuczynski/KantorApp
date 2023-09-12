
using KantorClient.Model;
using NLog;

namespace KantorClient.BLL.Models
{
    public class RateModel
    {
        public long Id { get; set; }
        public CurrencyModel Currency { get; set; }
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

        public RateModel()
        {
            Currency = new CurrencyModel();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(1);
        }

        public RateModel(Model.Rate rate)
        {
            Id = rate.Id;
            DefaultBuyRate = rate.DefaultBuyRate;
            MaximumBuyRate = rate.MaximumBuyRate;
            DefaultSellRate = rate.DefaultSellRate;
            MinimalSellRate = rate.MinimalSellRate;
            UseNbpSpread = rate.UseNbpSpread;
            Spread = rate.Spread;
            StartDate = rate.StartDate;
            EndDate = rate.EndDate;
            Valid = rate.Valid;
            Currency = new CurrencyModel(rate.Currency);
            ExternalId = rate.ExternalId ?? 0;
        }

        public static Rate Map(RateModel model) => new Rate
        {
            Id = model.Id,
            DefaultBuyRate = model.DefaultBuyRate,
            MaximumBuyRate = model.MaximumBuyRate,
            DefaultSellRate = model.DefaultSellRate,
            MinimalSellRate = model.MinimalSellRate,
            UseNbpSpread = model.UseNbpSpread,
            Spread = model.Spread,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Valid = model.Valid,
            Currency = model.Currency.Map(),
            ExternalId = model.Id
        };
    }
}
