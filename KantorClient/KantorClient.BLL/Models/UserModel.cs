using KantorServer.Model.Dtos;

namespace KantorClient.BLL.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public long UserPermissionId { get; set; }
        public string Permission { get; set; }
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
            if (user.Permission.Permissions != null)
            {
                Permission = string.Join(';', user.Permission.Permissions.Select(x => x.Key));
            }
            UserPermissionId = user.Permission.Id;
            Valid = user.Valid;
        }
    }
}
