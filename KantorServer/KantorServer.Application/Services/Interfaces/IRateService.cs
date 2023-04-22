using KantorServer.Model;
using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Services.Interfaces
{
    public interface IRateService : IService
    {
        Task<bool> AddEditRate(RateDto rate);
        Task<bool> RemoveRate(RateDto rate);
        Task<List<Rate>> GetAllRates();
    }
}
