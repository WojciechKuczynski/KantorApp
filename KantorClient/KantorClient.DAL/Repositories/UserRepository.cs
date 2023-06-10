using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.DAL.ServerCommunication;
using KantorClient.Model;
using KantorServer.Application.Requests;
using KantorServer.Application.Requests.Users;
using KantorServer.Application.Responses;
using KantorServer.Application.Responses.Users;
using KantorServer.Model.Dtos;
using Microsoft.EntityFrameworkCore;

namespace KantorClient.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        public async Task<UserDto> AddUser(UserDto user, string synchronizationKey)
        {
            var request = new AddEditUserRequest()
            {
                SynchronizationKey = synchronizationKey,
                User = user
            };

            var requestContext = new RequestContext("https://localhost:7254/users/add", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<AddEditUserRequest, AddEditUserResponse>(requestContext, request);

            return response.User;
        }

        public async Task<UserDto> EditUser(UserDto user, string synchronizationKey)
        {
            var request = new AddEditUserRequest()
            {
                SynchronizationKey = synchronizationKey,
                User = user
            };

            var requestContext = new RequestContext("https://localhost:7254/users/add", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<AddEditUserRequest, AddEditUserResponse>(requestContext, request);

            return response.User;
        }

        public async Task<List<UserDto>> GetUsers(string synchronizationKey)
        {
            var request = new GetAllUsersRequest()
            {
                SynchronizationKey = synchronizationKey
            };
            var requestContext = new RequestContext("https://localhost:7254/users/list", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<GetAllUsersRequest, GetAllUsersResponse>(requestContext, request);

            return response.Users;
        }

        public async Task<UserSession> SetPln(UserSession session, decimal value)
        {
            using var context = new DataContext();
            var sessionInDb = await context.UserSessions.FindAsync(session.Id);
            sessionInDb.Cash = value;
            await context.SaveChangesAsync();
            return sessionInDb;
        }

        public async Task<Model.UserSession> UserLogin(string username, string password)
        {
            var request = new LoginRequest()
            {
                Kantor = new KantorDto { Id = 1 },
                User = new UserDto { Login = username, Password = password }
            };
            var requestContext = new RequestContext("https://localhost:7254/session/login", RestSharp.Method.Post);

            var response = await ServerConnectionHandler.ExecuteFunction<LoginRequest, LoginResponse>(requestContext, request);
            if (response == null)
            {
                return null;
            }

            var session = new Model.UserSession() { LastAction = DateTime.Now, StartDate = DateTime.Now, SynchronizationKey = response.SynchronizationKey, UserId = 1 };
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
            return session;
        }
    }
}
