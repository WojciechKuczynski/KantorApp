using KantorServer.DAL;
using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Services.Interfaces
{
    public interface ISettingsService : IService
    {
        Task<bool> AddKantor(KantorDto kantor, CancellationToken token);
        Task<List<CurrencyDto>> GetCurrencies();
        Task<List<KantorDto>> GetKantors();
    }
}
