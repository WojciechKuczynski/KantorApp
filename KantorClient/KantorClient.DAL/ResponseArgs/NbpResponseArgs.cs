using KantorServer.Application.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.DAL.ResponseArgs
{
    [Serializable]
    public class NbpResponseArgs : BaseServerResponse
    {
        public List<Root> Root { get; set; }
    }

    [Serializable]
    public class Root
    {
        public string table { get; set; }
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public List<NBPRate> rates { get; set; }
    }

    [Serializable]
    public class NBPRate
    {
        public string currency { get; set; }
        public string code { get; set; }
        public decimal mid { get; set; }
    }
}
