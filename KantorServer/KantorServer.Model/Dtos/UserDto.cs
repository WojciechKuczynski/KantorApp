using KantorServer.Model.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Model.Dtos
{
    [Serializable]
    public class UserDto
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public UserPermission Permission { get; set; }
        public bool Valid { get; set; }

        public UserDto()
        {
            Id = 0;
            Login = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
        }

        public UserDto(User user) 
        {
            Id = user.Id;
            Login = user.Login;
            Password = user.Password;
            Name = user.Name;
            Permission = user.Permission;
            Valid = user.Valid;
        }

        public static List<UserDto> Map(List<User> users) => users.Select(x => new UserDto(x)).ToList();

        public User ConvertToEntity()
        {
            var user = new User();
            if (Id > 0)
                user.Id = Id;
            user.Login = Login;
            user.Password = Password;
            user.Name = Name;
            user.Permission = Permission;
            user.Valid = Valid;
            return user;
        }
    }
}
