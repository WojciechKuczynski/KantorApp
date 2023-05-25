using KantorServer.Application.Responses;
using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Services.Interfaces
{
    public interface IUserService : IService
    {
        Task<UserDto> AddEditUser(UserDto user);
        Task<LoginResponse> UserLogin(UserDto user, KantorDto kantor);
        Task<List<UserDto>> GetUsers();
    }
}
