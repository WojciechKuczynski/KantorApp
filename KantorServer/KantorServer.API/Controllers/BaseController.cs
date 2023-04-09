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
    }
}
