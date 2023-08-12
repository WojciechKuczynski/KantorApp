using KantorServer.Application.Responses;
using KantorServer.Model.Dtos;

namespace KantorServer.Application.Requests.Users
{
    [Serializable]
    public class AddEditPermissionResponse : BaseServerResponse
    {
        public AddEditPermissionResponse(bool isCorrect, string? successMsg = null, string? failMsg = null) : base(isCorrect, successMsg, failMsg)
        {
        }

        public AddEditPermissionResponse()
        {
            
        }

        public UserPermissionDto Permission { get; set; }
    }
}
