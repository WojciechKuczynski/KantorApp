using KantorServer.Model.Dtos;

namespace KantorClient.DAL.ResponseArgs
{
    public class ReportsSettingsResponseArgs
    {
        public List<KantorDto> Kantors { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
