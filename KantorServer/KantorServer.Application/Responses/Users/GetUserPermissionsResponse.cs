using KantorServer.Model.Dtos;

namespace KantorServer.Application.Responses.Users
{
    [Serializable]
    public class GetUserPermissionsResponse : BaseServerResponse
    {
        public GetUserPermissionsResponse(bool iscorrect, string? successMsg = null, string? failMsg = null) : base(iscorrect, successMsg, failMsg)
        {
        }

        public GetUserPermissionsResponse()
        {
        }

        public List<UserPermissionDto> UserPermissions { get; set; }
    }
}
