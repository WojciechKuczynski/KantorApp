using KantorServer.Application.Responses;
using KantorServer.Model.Dtos;

namespace KantorServer.Application.Services.Interfaces
{
    public interface IUserService : IService
    {
        Task<UserDto> AddEditUser(UserDto user);
        Task<LoginResponse> UserLogin(UserDto user, KantorDto kantor);
        Task<List<UserDto>> GetUsers();
    }
}
