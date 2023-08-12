using KantorServer.Model.Dtos;

namespace KantorServer.Application.Requests.Users
{
    [Serializable]
    public class AddEditPermissionRequest : BaseServerRequest
    {
        public UserPermissionDto Permission { get; set; }
    }
}
