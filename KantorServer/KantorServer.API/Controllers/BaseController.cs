using KantorServer.Application.Requests;
using KantorServer.Application.Responses;
using KantorServer.Application.Services.Interfaces;
using KantorServer.Model;

namespace KantorServer.API.Controllers
{
    public abstract class BaseController
    {
        protected readonly ISessionService _sessionService;
        private readonly IUserPermissionService _userPermissionService;

        public BaseController(ISessionService sessionService, IUserPermissionService userPermissionService)
        {
            _sessionService = sessionService;
            _userPermissionService = userPermissionService;
        }

        protected async Task<UserSession> CheckSession(string hash)
        {
            return await _sessionService.CheckSessionToken(hash);
        }

        protected async Task<T> CheckRequestArgs<T>(BaseServerRequest request, IEnumerable<string> permissionKey = null) where T : BaseServerResponse
        {
            var userSession = await CheckSession(request.SynchronizationKey);
            if (userSession == null)
            {
                var res = Activator.CreateInstance<T>();
                res.ResponseText = "Podano niepoprawny hash. Proszę przelogować aplikację!";
                res.ResponseType = Model.Consts.ServerResponseType.Error;
                return await Task.FromResult(res);
            }

            if (permissionKey != null && !await _userPermissionService.HasUserPermission(userSession.User.Id, permissionKey))
            {
                var res = Activator.CreateInstance<T>();
                res.ResponseText = "Użytkownik nie posiada uprawnień do tej akcji!";
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
