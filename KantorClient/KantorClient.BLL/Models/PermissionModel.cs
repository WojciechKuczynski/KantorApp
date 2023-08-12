using KantorServer.Model.Dtos;
using System.ComponentModel;

namespace KantorClient.BLL.Models
{
    public class PermissionModel
    {
        public long Id { get; private set; }
        public string Key { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Module { get; private set; }

        // for UI only
        public bool ActiveInPermission { get; set; }

        public PermissionModel()
        {
            
        }
        public PermissionModel(PermissionDto p)
        {
            Id = p.Id;
            Key = p.Key;
            Name = p.Name;
            Description = p.Description;
            Module = p.Module;
        }
        public PermissionDto ToDto()
        {
            var dto = new PermissionDto
            {
                Id = Id,
                Key = Key,
                Name = Name,
                Description = Description,
                Module = Module
            };
            return dto;
        }
    }
}
