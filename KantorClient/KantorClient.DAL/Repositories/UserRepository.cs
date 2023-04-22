using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.DAL.ServerCommunication;
using KantorClient.Model;
using KantorServer.Application.Requests;
using KantorServer.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        public async Task<UserSession> UserLogin(string username, string password)
        {
            var request = new LoginRequest()
            {
                Kantor = new KantorServer.Model.Dtos.KantorDto { Id = 1 },
                User = new KantorServer.Model.Dtos.UserDto { Login = username, Password = password }
            };
            var requestContext = new RequestContext("https://localhost:7254/session/login", RestSharp.Method.Post);

            var response = await ServerConnectionHandler.ExecuteFunction<LoginRequest, LoginResponse>(requestContext, request);

            var session = new UserSession() { LastAction = DateTime.Now, StartDate = DateTime.Now, SynchronizationKey = response.SynchronizationKey, UserId = 1 };
            return session;
        }
    }
}
