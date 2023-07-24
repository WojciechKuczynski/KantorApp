using KantorServer.Application.Requests.Kantor;
using KantorServer.Application.Responses;
using KantorServer.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KantorServer.API.Controllers
{
    [ApiController]
    [Route("/settings")]
    public class SettingsController : BaseController
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService, ISessionService sessionService, IUserPermissionService userPermissionService) : base(sessionService, userPermissionService)
        {
            _settingsService = settingsService;
        }

        [HttpPost("addEditKantor")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<BaseServerResponse> AddKantor(AddEditKantorRequest request)
        {

            if (await CheckSession(request.SynchronizationKey) == null)
            {
                return await Task.FromResult(new BaseServerResponse(false, "", "Podano niepoprawny hash. Proszę przelogować aplikację!"));
            }

            if (request.Kantor == null)
            {
                return await Task.FromResult(new BaseServerResponse(false, "", "Podano niepoprawny Kantor"));
            }

            var res = await _settingsService.AddKantor(request.Kantor, new CancellationToken());
            return new BaseServerResponse(res, "Pomyślnie dodano nowy Kantor", "Nie udało się dodać Kantoru");
        }
    }
}
