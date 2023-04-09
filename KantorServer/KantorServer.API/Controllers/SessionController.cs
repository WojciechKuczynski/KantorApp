using KantorServer.Application.Requests;
using KantorServer.Application.Responses;
using KantorServer.Application.Services.Interfaces;
using KantorServer.Model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KantorServer.API.Controllers
{
    [ApiController]
    [Route("/session")]
    public class SessionController : BaseController
    {
        private readonly IUserService _userService;
        public SessionController(ISessionService sessionService, IUserService userService) : base(sessionService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            if (request == null)
            {
                return new LoginResponse(false, "", "Nie udało się zalogować do systemu!");
            }

            return await _userService.UserLogin(request.User, request.Kantor);
        }
    }
}
