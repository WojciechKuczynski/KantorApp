namespace KantorServer.Model.Dtos
{
    [Serializable]
    public class UserPermissionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<PermissionDto> Permissions { get; set; }

        public UserPermissionDto()
        {
            Id = 0;
            Name = string.Empty;
            Permissions = new List<PermissionDto>();
        }
        public UserPermissionDto(UserPermission up)
        {
            Id = up.Id;
            Name = up.Name;
            Permissions = PermissionDto.Map(up.Permissions.ToList());
        }

        public UserPermission ConvertToEntity()
        {
            var up = new UserPermission();
            if (Id > 0)
            {
                up.Id = Id;
            }

            up.Name = Name;
            if (Permissions != null)
            {
                up.Permissions = Permissions.Select(x => x.ConvertToEntity()).ToList();
            }

            return up;
        }
    }
}
