using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.DAL.ResponseArgs;
using KantorClient.DAL.ServerCommunication;
using KantorClient.Model;
using KantorClient.Model.Consts;
using KantorServer.Application.Requests;
using KantorServer.Application.Requests.Users;
using KantorServer.Application.Responses;
using KantorServer.Application.Responses.Users;
using KantorServer.Model.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace KantorClient.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly IConfigurationRepository _configurationRepository;

        public UserRepository(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<UserDto> AddUser(UserDto user, string synchronizationKey)
        {
            var request = new AddEditUserRequest()
            {
                SynchronizationKey = synchronizationKey,
                User = user
            };

            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/users/add", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<AddEditUserRequest, AddEditUserResponse>(requestContext, request);

            return response.User;
        }

        public async Task<UserPermissionDto> AddEditUserPermission(UserPermissionDto dto, string synchronizationKey)
        {
            var request = new AddEditPermissionRequest()
            {
                SynchronizationKey = synchronizationKey,
                Permission = dto
            };

            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/users/userpermission/add", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<AddEditPermissionRequest, AddEditPermissionResponse>(requestContext, request);

            return response.Permission;
        }

        public async Task<UserDto> EditUser(UserDto user, string synchronizationKey)
        {
            var request = new AddEditUserRequest()
            {
                SynchronizationKey = synchronizationKey,
                User = user
            };

            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/users/add", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<AddEditUserRequest, AddEditUserResponse>(requestContext, request);

            return response.User;
        }

        public async Task<IEnumerable<PermissionDto>> GetPermissions(string synchronizationKey)
        {
            var request = new GetPermissionsRequest
            {
                SynchronizationKey = synchronizationKey
            };
            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/users/permission/list", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<GetPermissionsRequest, GetPermissionsResponse>(requestContext, request);
            return response.Permissions;
        }

        public async Task<IEnumerable<UserPermissionDto>> GetUserPermissions(string synchronizationKey)
        {
            var request = new GetUserPermissionsRequest
            {
                SynchronizationKey = synchronizationKey
            };
            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/users/userpermission/list", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<GetUserPermissionsRequest, GetUserPermissionsResponse>(requestContext, request);
            return response.UserPermissions;
        }

        public async Task<List<UserDto>> GetUsers(string synchronizationKey)
        {
            var request = new GetAllUsersRequest()
            {
                SynchronizationKey = synchronizationKey
            };
            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/users/list", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<GetAllUsersRequest, GetAllUsersResponse>(requestContext, request);

            return response.Users;
        }

        public async Task<bool> SavePermissionsToUserPermission(UserPermissionDto userPerm, List<PermissionDto> perms, string synchronizationKey)
        {
            userPerm.Permissions = perms;
            var request = new AddEditPermissionRequest()
            {
                SynchronizationKey = synchronizationKey,
                Permission = userPerm
            };
            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/users/userpermission/edit", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<AddEditPermissionRequest, AddEditPermissionResponse>(requestContext, request);
            return response.ResponseType == KantorServer.Model.Consts.ServerResponseType.Success;
        }

        public async Task<UserSession> SetPln(UserSession session, decimal value)
        {
            using var context = new DataContext();
            var sessionInDb = await context.UserSessions.FindAsync(session.Id);
            if (sessionInDb != null)
            {
                sessionInDb.Cash = value;
            }

            await context.SaveChangesAsync();
            return sessionInDb;
        }

        public async Task<LoginResponseArgs> UserLogin(string username, string password, string kantor)
        {
            var request = new LoginRequest()
            {
                Kantor = new KantorDto { IdentificationKey = kantor },
                User = new UserDto { Login = username, Password = password, Permission = new UserPermissionDto() }
            };
            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/session/login", RestSharp.Method.Post);

            var response = await ServerConnectionHandler.ExecuteFunction<LoginRequest, LoginResponse>(requestContext, request);
            if (response == null)
            {
                return new LoginResponseArgs
                {
                    Error = true,
                    ErrorMessage = "Nie można połączyć się z serwerem!",
                };
            }

            if (response.ResponseType == KantorServer.Model.Consts.ServerResponseType.Error)
            {
                return new LoginResponseArgs
                {
                    Error = true,
                    ErrorMessage = response.ResponseText,
                };
            }

            var session = new Model.UserSession() { LastAction = DateTime.Now, StartDate = DateTime.Now, 
                                                    SynchronizationKey = response.SynchronizationKey, UserId = response.UserId, 
                                                    Name = response.Name, UserPermission = string.Join(';',response.Permission.Permissions.Select(x => x.Key)),
                                                    KantorId = response.Kantor.Id};
            using (var context = new DataContext())
            {
                var lastSession = await context.UserSessions.OrderByDescending(x => x.StartDate).FirstOrDefaultAsync();
                if (lastSession != null)
                {
                    session.Cash = lastSession.Cash;
                }

                await context.UserSessions.AddAsync(session);
                await context.SaveChangesAsync();
            }
            return new LoginResponseArgs
            {
                Error = false,
                LoggedSession = session,
            };
        }
    }
}
