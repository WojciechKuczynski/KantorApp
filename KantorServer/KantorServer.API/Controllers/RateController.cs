using KantorServer.Application.Requests.Rates;
using KantorServer.Application.Responses;
using KantorServer.Application.Services;
using KantorServer.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KantorServer.API.Controllers
{
    [ApiController]
    [Route("/rates")]
    public class RateController : BaseController
    {
        private readonly IRateService _rateService;

        public RateController(ISessionService sessionService, IRateService rateService) : base(sessionService)
        {
            _rateService = rateService;
        }

        [HttpPost("addRate")]
        public async Task<BaseServerResponse> AddEditRate(AddEditRateRequest request) 
        {
            var checkRes = await CheckRequestArgs(request);
            if (checkRes != null) { return checkRes; }

            var res = await _rateService.AddEditRate(request.Rate);
            return new BaseServerResponse(res, "Pomyślnie dodano nowy Kurs", "Nie udało się dodać Kursu");
        }

        [HttpPost("editRate")]
        public async Task<BaseServerResponse> EditRate(AddEditRateRequest request)
        {
            var checkRes = await CheckRequestArgs(request);
            if (checkRes != null) { return checkRes; }

            var res = await _rateService.AddEditRate(request.Rate);
            return new BaseServerResponse(res, "Pomyślnie edytowano Kurs", "Nie udało się edytować Kursu");
        }

        [HttpPost("removeRate")]
        public async Task<BaseServerResponse> RemoveRate(AddEditRateRequest request)
        {
            var checkRes = await CheckRequestArgs(request);
            if (checkRes != null) { return checkRes; }

            var res = await _rateService.RemoveRate(request.Rate);
            return new BaseServerResponse(res, "Pomyślnie usunięto Kurs", "Nie udało się usunąć Kursu");
        }

        private async Task<BaseServerResponse> CheckRequestArgs(AddEditRateRequest request)
        {
            if (!await CheckSession(request.SynchronizationKey))
            {
                return await Task.FromResult(new BaseServerResponse(false, "", "Podano niepoprawny hash. Proszę przelogować aplikację!"));
            }

            if (request == null)
            {
                return await Task.FromResult(new BaseServerResponse(false, "", "Podano niepoprawny Kurs!"));
            }

            return null;
        }
    }
}
