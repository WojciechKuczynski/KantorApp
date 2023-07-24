using System.Security;

namespace KantorServer.Model.Dtos
{
    [Serializable]
    public class PermissionDto
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }

        public PermissionDto()
        {
            Id = 0;
            Key = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Module = string.Empty;
        }

        public PermissionDto(Permission p)
        {
            Id = p.Id;
            Key = p.Key;
            Name = p.Name;
            Description = p.Description;
            Module = p.Module;
        }

        public static List<PermissionDto> Map(List<Permission> permissions)
            => permissions.Select(x => new PermissionDto(x)).ToList();

        public Permission ConvertToEntity()
        {
            var perm = new Permission();
            if (Id > 0 )
                perm.Id = Id;
            perm.Name = Name;
            perm.Key = Key;
            perm.Description = Description;
            perm.Module = Module;
            return perm;
        }
    }
}
