using KantorServer.Model.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Model
{
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public UserPermission Permission { get; set; }
        public bool Valid { get; set; }

        public User()
        {
            
        }
        public User(string login, string password, string name)
        {
            Login = login;
            Password = password;
            Name = name;
        }
    }
}
