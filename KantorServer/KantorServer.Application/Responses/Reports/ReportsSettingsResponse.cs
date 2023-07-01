using KantorServer.Model.Dtos;

namespace KantorServer.Application.Responses.Reports
{
    [Serializable]
    public class ReportsSettingsResponse : BaseServerResponse
    {
        public ReportsSettingsResponse(bool isCorrect, string? successMsg = null, string? failMsg = null) : base(isCorrect, successMsg, failMsg)
        {
        }

        public ReportsSettingsResponse()
        {
            
        }

        public List<KantorDto> Kantors { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
