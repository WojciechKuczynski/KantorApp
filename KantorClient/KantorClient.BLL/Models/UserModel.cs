using KantorClient.Model.Consts;
using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.BLL.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserPermission Permission { get; set; }
        public bool Valid { get; set; }

        public UserModel()
        {
            
        }
        public UserModel(UserDto user)
        {
            if (user.Id != 0)
            {
                Id = user.Id;
            }

            Name = user.Name;
            Login = user.Login;
            Password = user.Password;
            Permission = (UserPermission) user.Permission;
            Valid = user.Valid;
        }
    }
}
