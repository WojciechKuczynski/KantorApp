using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Model
{
    public abstract class BaseModel
    {
        public long Id { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
