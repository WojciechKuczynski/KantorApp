using KantorServer.Application.Requests;
using KantorServer.Application.Requests.Rates;
using KantorServer.Application.Responses;
using KantorServer.Application.Responses.Rates;
using KantorServer.Application.Services.Interfaces;
using KantorServer.Model.Dtos;
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

        [HttpPost("synchronizeRates")]
        public async Task<AddEditRateResponse> SynchronizeRates(AddEditRateRequest request)
        {
            return null;
        }

        [HttpPost("addRate")]
        public async Task<AddEditRateResponse> AddEditRate(AddEditRateRequest request)
        {
            var checkRes = await CheckRequestArgs<AddEditRateResponse>(request);
            if (checkRes != null) { return checkRes; }

            var res = await _rateService.AddEditRate(request.Rate);
            return new AddEditRateResponse(res, "Pomyślnie dodano nowy Kurs", "Nie udało się dodać Kursu")
            {
                Rate = res
            };
        }

        [HttpPost("editRate")]
        public async Task<AddEditRateResponse> EditRate(AddEditRateRequest request)
        {
            var checkRes = await CheckRequestArgs<AddEditRateResponse>(request);
            if (checkRes != null) { return checkRes; }

            var res = await _rateService.AddEditRate(request.Rate);
            return new AddEditRateResponse(res, "Pomyślnie edytowano Kurs", "Nie udało się edytować Kursu");
        }

        [HttpPost("removeRate")]
        public async Task<BaseServerResponse> RemoveRate(AddEditRateRequest request)
        {
            var checkRes = await CheckRequestArgs<BaseServerResponse>(request);
            if (checkRes != null) { return checkRes; }

            var res = await _rateService.RemoveRate(request.Rate);
            return new BaseServerResponse(res, "Pomyślnie usunięto Kurs", "Nie udało się usunąć Kursu");
        }

        [HttpPost("all")]
        public async Task<GetAllRatesResponse> GetAllRates(GetAllRatesRequest request)
        {
            var checkRes = await CheckRequestArgs<GetAllRatesResponse>(request);
            if (checkRes != null) { return checkRes; }

            var res = await _rateService.GetAllRates();
            return new GetAllRatesResponse(true, "Pomyślnie zwrócono kursy", "Nie udało się pobrać kursów") { Rates = RateDto.Map(res) };
        }
    }
}
