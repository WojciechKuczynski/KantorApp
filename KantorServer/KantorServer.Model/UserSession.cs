using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Model
{
    public class UserSession : BaseModel
    {
        public virtual Kantor Kantor { get; set; }
        public virtual User User { get; set; }
        public DateTime StartDate { get; set; }
        public string SynchronizationKey { get; set; }

        public UserSession()
        {
            
        }
        public UserSession(Kantor kantor, User user, DateTime startDate, string synchronizationKey)
        {
            Kantor = kantor;
            User = user;
            StartDate = startDate;
            SynchronizationKey = synchronizationKey;
        }
    }
}
