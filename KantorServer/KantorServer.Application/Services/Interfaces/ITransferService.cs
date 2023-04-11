using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Services.Interfaces
{
    public interface ITransferService : IService
    {
        public Task<List<TransferDto>> GetAllTransfers();
    }
}
