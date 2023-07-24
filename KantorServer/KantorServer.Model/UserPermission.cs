namespace KantorServer.Model
{
    public class UserPermission : BaseModel
    {
        public string Name { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
