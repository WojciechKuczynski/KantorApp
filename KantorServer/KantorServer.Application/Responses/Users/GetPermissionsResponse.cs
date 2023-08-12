using KantorServer.Model.Dtos;

namespace KantorServer.Application.Responses.Users
{
    [Serializable]
    public class GetPermissionsResponse : BaseServerResponse
    {
        public GetPermissionsResponse(bool isCorrect, string? successMsg = null, string? failMsg = null) : base(isCorrect, successMsg, failMsg)
        {
        }

        public GetPermissionsResponse()
        {
        }

        public List<PermissionDto> Permissions { get; set; }
    }
}
