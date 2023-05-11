using KantorServer.Application.Requests;
using KantorServer.Application.Responses;
using KantorServer.Application.Services.Interfaces;

namespace KantorServer.API.Controllers
{
    public abstract class BaseController
    {
        protected readonly ISessionService _sessionService;

        public BaseController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        protected async Task<bool> CheckSession(string hash)
        {
            return await _sessionService.CheckSessionToken(hash);
        }

        protected async Task<T> CheckRequestArgs<T>(BaseServerRequest request) where T : BaseServerResponse
        {
            if (!await CheckSession(request.SynchronizationKey))
            {
                var res = Activator.CreateInstance<T>();
                res.ResponseText = "Podano niepoprawny hash. Proszę przelogować aplikację!";
                res.ResponseType = Model.Consts.ServerResponseType.Error;
                return await Task.FromResult(res);
            }

            if (request == null)
            {
                var res = Activator.CreateInstance<T>();
                res.ResponseText = "Podano niepoprawne dane!";
                res.ResponseType = Model.Consts.ServerResponseType.Error;
                return await Task.FromResult(res);
            }

            return null;
        }
    }
}
