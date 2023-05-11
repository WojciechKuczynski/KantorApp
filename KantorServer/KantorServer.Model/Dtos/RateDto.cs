using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Model.Dtos
{
    [Serializable]
    public class RateDto
    {
        public long Id { get; set; }
        public CurrencyDto Currency { get; set; }
        public decimal DefaultBuyRate { get; set; }
        public decimal MinimalBuyRate { get; set; }
        public decimal DefaultSellRate { get; set; }
        public decimal MinimalSellRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Valid { get; set; }
        public long ExternalId { get; set; }

        public RateDto()
        {
            
        }
        public RateDto(Rate rate)
        {
            Id = rate.Id;
            Currency = new CurrencyDto(rate.Currency);
            DefaultBuyRate = rate.DefaultBuyRate;
            MinimalBuyRate = rate.MinimalBuyRate;
            DefaultSellRate = rate.DefaultSellRate;
            MinimalSellRate = rate.MinimalSellRate;
            StartDate = rate.StartDate;
            EndDate = rate.EndDate;
            Valid = rate.Valid;
            ExternalId = rate.ExternalId;
        }

        public static List<RateDto> Map(IEnumerable<Rate> rates) => rates.Select(r => new RateDto(r)).ToList();

        public Rate ConvertToEntity()
        {
            var rate = new Rate();
            if (Id > 0)
                rate.Id = Id;
            rate.Currency = Currency.ConvertToEntity();
            rate.DefaultBuyRate = DefaultBuyRate;
            rate.MinimalBuyRate = MinimalBuyRate;
            rate.DefaultSellRate = DefaultSellRate;
            rate.MinimalSellRate = MinimalSellRate;
            rate.StartDate = StartDate;
            rate.EndDate = EndDate;
            rate.Valid = Valid;
            rate.ExternalId = ExternalId;
            return rate;
        }
    }
}
