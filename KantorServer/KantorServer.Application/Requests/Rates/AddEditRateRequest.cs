using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Requests.Rates
{
    public class AddEditRateRequest : BaseServerRequest
    {
        public RateDto Rate { get; set; }
    }
}
