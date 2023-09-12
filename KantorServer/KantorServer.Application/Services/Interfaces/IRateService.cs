using KantorServer.Model;
using KantorServer.Model.Dtos;

namespace KantorServer.Application.Services.Interfaces
{
    public interface IRateService : IService
    {
        Task<RateDto> AddEditRate(RateDto rate);
        Task<bool> RemoveRate(RateDto rate);
        Task<List<Rate>> GetAllRates();
    }
}
