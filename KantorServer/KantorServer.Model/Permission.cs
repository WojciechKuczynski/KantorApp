using System.Collections.Generic;

namespace KantorServer.Model
{
    public class Permission : BaseModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }

        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}
