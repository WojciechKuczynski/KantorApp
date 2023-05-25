using KantorServer.Application.Requests.Kantor;
using KantorServer.Application.Requests.Users;
using KantorServer.Application.Responses;
using KantorServer.Application.Services.Interfaces;
using KantorServer.Model;
using KantorServer.Model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KantorServer.API.Controllers
{
    [ApiController]
    [Route("/settings")]
    public class SettingsController : BaseController
    {
        private readonly ISettingsService _settingsService;
        private readonly IUserService _userService;

        public SettingsController(ISettingsService settingsService,ISessionService sessionService, IUserService userService) : base(sessionService)
        {
            _settingsService = settingsService;
            _userService = userService;
        }

        [HttpPost("addEditKantor")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<BaseServerResponse> AddKantor(AddEditKantorRequest request)
        {
            if (!await CheckSession(request.SynchronizationKey))
            {
                return await Task.FromResult(new BaseServerResponse(false, "", "Podano niepoprawny hash. Proszę przelogować aplikację!"));
            }

            if (request.Kantor == null) 
            {
                return await Task.FromResult(new BaseServerResponse(false,"", "Podano niepoprawny Kantor"));
            }

            var res = await _settingsService.AddKantor(request.Kantor, new CancellationToken());
            return new BaseServerResponse(res, "Pomyślnie dodano nowy Kantor", "Nie udało się dodać Kantoru");
        }
    }
}
