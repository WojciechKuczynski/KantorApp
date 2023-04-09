using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Requests.Kantor
{
    public class AddEditKantorRequest : BaseServerRequest
    {
        public KantorDto Kantor { get; set; }
    }
}
