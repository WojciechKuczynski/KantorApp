using KantorServer.Application.Requests;
using KantorServer.Application.Requests.Users;
using KantorServer.Application.Responses.Users;
using KantorServer.Application.Services.Interfaces;
using KantorServer.Model.Consts;
using Microsoft.AspNetCore.Mvc;

namespace KantorServer.API.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserPermissionService _userPermissionService;
        public UserController(ISessionService sessionService, IUserService userService, IUserPermissionService userPermissionService) : base(sessionService, userPermissionService)
        {
            _userService = userService;
            _userPermissionService = userPermissionService;
        }

        [HttpPost("add")]
        public async Task<AddEditUserResponse> AddEditUser(AddEditUserRequest request)
        {
            var checkRes = await CheckRequestArgs<AddEditUserResponse>(request, new[] { PermissionKeys.User.AddUser });
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
            var checkRes = await CheckRequestArgs<GetAllUsersResponse>(request, new[] { PermissionKeys.User.ListUser });
            if (checkRes != null) { return checkRes; }

            var res = await _userService.GetUsers();
            return new GetAllUsersResponse(true, "Pomyślnie zwrócono kursy", "Nie udało się pobrać kursów") { Users = res };
        }

        [HttpPost("userpermission/list")]
        public async Task<GetUserPermissionsResponse> GetUserPermissions(GetUserPermissionsRequest request)
        {
            var checkRes = await CheckRequestArgs<GetUserPermissionsResponse>(request, new[] { PermissionKeys.Permission.ListPermission });
            if (checkRes != null) { return checkRes; }
            var res = await _userPermissionService.GetUserPermissions();
            var result = new GetUserPermissionsResponse(res?.Any() ?? false, "Pomyślne zwrócono uprawnienia", "Nie udało się pobrać uprawnień")
            {
                UserPermissions = res
            };
            return result;
        }

        [HttpPost("permission/list")]
        public async Task<GetPermissionsResponse> GetPermissions(GetPermissionsRequest request)
        {
            var checkRes = await CheckRequestArgs<GetPermissionsResponse>(request, new[] { PermissionKeys.Permission.ListPermission });
            if (checkRes != null) { return checkRes; }
            var res = await _userPermissionService.GetPermissions();
            return new GetPermissionsResponse(res?.Any() ?? false, "Pomyślne zwrócono uprawnienia", "Nie udało się pobrać uprawnień")
            {
                Permissions = res
            };
        }

        [HttpPost("userpermission/add")]
        public async Task<AddEditPermissionResponse> AddEditPermission(AddEditPermissionRequest request)
        {
            var checkRes = await CheckRequestArgs<AddEditPermissionResponse>(request, new[] { PermissionKeys.Permission.AddPermission });
            if (checkRes != null) { return checkRes; }
            var res = await _userPermissionService.CreateUserPermission(request.Permission);
            return new AddEditPermissionResponse(res != null, "Pomyślnie dodano uprawnienie", "Nie udało się dodać uprawnienia")
            {
                Permission = res
            };
        }

        [HttpPost("userpermission/edit")]
        public async Task<AddEditPermissionResponse> EditPermission(AddEditPermissionRequest request)
        {
            var checkRes = await CheckRequestArgs<AddEditPermissionResponse>(request, new[] { PermissionKeys.Permission.EditPermission });
            if (checkRes != null) { return checkRes; }
            var res = await _userPermissionService.EditUserPermission(request.Permission);
            return new AddEditPermissionResponse(res != null, "Pomyślnie edytowano uprawnienie", "Nie udało się edytować uprawnienia")
            {
                Permission = res
            };
        }
    }
}
