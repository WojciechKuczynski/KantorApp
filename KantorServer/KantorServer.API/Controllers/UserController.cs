using KantorServer.Application.Requests.Rates;
using KantorServer.Application.Requests.Users;
using KantorServer.Application.Responses.Rates;
using KantorServer.Application.Responses.Users;
using KantorServer.Application.Services;
using KantorServer.Application.Services.Interfaces;
using KantorServer.Model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KantorServer.API.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(ISessionService sessionService, IUserService userService) : base(sessionService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public async Task<AddEditUserResponse> AddEditUser(AddEditUserRequest request)
        {
            var checkRes = await CheckRequestArgs<AddEditUserResponse>(request);
            if (checkRes != null) { return checkRes; }

            var res = await _userService.AddEditUser(request.User);
            return new AddEditUserResponse(res, "Pomyślnie dodano nowy Kurs", "Nie udało się dodać Kursu")
            {
                User = res
            };
        }

        [HttpPost("list")]
        public async Task<GetAllUsersResponse> GetUserList(GetAllUsersRequest request)
        {
            var checkRes = await CheckRequestArgs<GetAllUsersResponse>(request);
            if (checkRes != null) { return checkRes; }

            var res = await _userService.GetUsers();
            return new GetAllUsersResponse(true, "Pomyślnie zwrócono kursy", "Nie udało się pobrać kursów") { Users = res };
        }
    }
}
