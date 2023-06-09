using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Model
{
    public class UserSession : BaseModel
    {
        // Id z bazy servera
        public long UserId { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime? LastAction { get; set; }
        public string SynchronizationKey { get; set; }
        public decimal Cash { get; set; }
    }
}
