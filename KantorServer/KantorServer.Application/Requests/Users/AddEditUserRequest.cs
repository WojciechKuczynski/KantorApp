using KantorServer.Model.Dtos;

namespace KantorServer.Application.Requests.Users
{
    [Serializable]
    public class AddEditUserRequest : BaseServerRequest
    {
        public UserDto User { get; set; }
    }
}
