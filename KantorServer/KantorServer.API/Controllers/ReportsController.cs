using KantorServer.Application.Requests;
using KantorServer.Application.Requests.Reports;
using KantorServer.Application.Responses.Reports;
using KantorServer.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KantorServer.API.Controllers
{
    [ApiController]
    [Route("/reports")]
    public class ReportsController : BaseController
    {
        private readonly ISettingsService _settingsService;
        private readonly IUserService _userService;
        public ReportsController(ISessionService sessionService, ISettingsService settingsService, IUserService userService) : base(sessionService)
        {
            _settingsService = settingsService;
            _userService = userService;
        }

        [HttpPost("settings")]
        public async Task<ReportsSettingsResponse> GetReportsInformation(ReportsSettingsRequest request)
        {
            var checkRes = await CheckRequestArgs<ReportsSettingsResponse>(request);
            if (checkRes != null) { return checkRes; }

            var kantors = await _settingsService.GetKantors();
            var users = await _userService.GetUsers();

            return new ReportsSettingsResponse(kantors != null && users != null, "", "Nie znaleziono Kantorów ani użytkowników")
            {
                Kantors = kantors,
                Users = users
            };
        }
    }
}
