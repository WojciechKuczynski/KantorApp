using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Model
{
    public class Kantor : BaseModel
    {
        public string Name { get; set; }
        public string IdentificationKey { get; set; }

        public Kantor()
        {
            
        }
        public Kantor(string name,string indetificationKey)
        {
            Name = name;
            IdentificationKey = indetificationKey;
        }
    }
}
