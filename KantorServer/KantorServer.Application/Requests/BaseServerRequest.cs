using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Requests
{
    public abstract class BaseServerRequest
    {
        public string SynchronizationKey { get; set; }

        public BaseServerRequest()
        {
            SynchronizationKey = string.Empty;
        }
    }
}
