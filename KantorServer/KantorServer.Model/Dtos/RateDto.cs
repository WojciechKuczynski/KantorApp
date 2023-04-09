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
        public decimal DefaultRate { get; set; }
        public decimal MinimalRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Valid { get; set; }

        public RateDto()
        {
            
        }
        public RateDto(Rate rate)
        {
            Id = rate.Id;
            Currency = new CurrencyDto(rate.Currency);
            DefaultRate = rate.DefaultRate;
            MinimalRate = rate.MinimalRate;
            StartDate = rate.StartDate;
            EndDate = rate.EndDate;
            Valid = rate.Valid;
        }

        public static List<RateDto> Map(IEnumerable<Rate> rates) => rates.Select(r => new RateDto(r)).ToList();

        public Rate ConvertToEntity()
        {
            var rate = new Rate();
            if (Id > 0)
                rate.Id = Id;
            rate.Currency = Currency.ConvertToEntity();
            rate.DefaultRate = DefaultRate;
            rate.MinimalRate = MinimalRate;
            rate.StartDate = StartDate;
            rate.EndDate = EndDate;
            rate.Valid = Valid;
            return rate;
        }
    }
}
