using KantorServer.Model.Dtos;

namespace KantorClient.BLL.Models
{
    public class UserPermissionModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<PermissionModel> Permissions { get; set; }

        public UserPermissionModel(UserPermissionDto up)
        {
            Id = up.Id;
            Name = up.Name;
            Permissions = up.Permissions.Select(x => new PermissionModel(x)).ToList();
        }

        public UserPermissionModel()
        {
            Permissions = new List<PermissionModel>();
        }
        public UserPermissionDto ToDto()
        {
            var dto = new UserPermissionDto
            {
                Id = Id,
                Name = Name,
                Permissions = Permissions?.Select(x => x.ToDto()).ToList() ?? new List<PermissionDto>()
            };
            return dto;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
