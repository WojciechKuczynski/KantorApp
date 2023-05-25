﻿using KantorServer.Application.Requests.Currencies;
using KantorServer.Application.Responses;
using KantorServer.Application.Responses.Currencies;
using KantorServer.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KantorServer.API.Controllers
{
    [ApiController]
    [Route("/currencies")]
    public class CurrencyController : BaseController
    {
        private readonly ISettingsService _settingsService;

        public CurrencyController(ISessionService sessionService, ISettingsService settingsService) : base(sessionService)
        {
            _settingsService = settingsService;
        }

        [HttpPost("all")]
        public async Task<GetAllCurrenciesResponse> GetAllCurrencies(GetAllCurrenciesRequest request)
        {
            var checkRes = await CheckRequestArgs<GetAllCurrenciesResponse>(request);
            if (checkRes != null) { return checkRes; }

            var currencies = await _settingsService.GetCurrencies();

            return new GetAllCurrenciesResponse(true) { Currencies = currencies };
        }
    }
}