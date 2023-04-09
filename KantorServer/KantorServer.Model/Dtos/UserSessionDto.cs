using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Model.Dtos
{
    [Serializable]
    public class UserSessionDto
    {
        public long Id { get; set; }
        public string SynchronizationKey { get; set; }

        public UserSessionDto()
        {
            
        }
        public UserSessionDto(UserSession userSession)
        {
            Id = userSession.Id;
            SynchronizationKey = userSession.SynchronizationKey;
        }
    }
}
