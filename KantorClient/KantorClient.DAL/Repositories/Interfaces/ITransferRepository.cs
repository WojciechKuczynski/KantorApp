using KantorClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.DAL.Repositories.Interfaces
{
    public interface ITransferRepository
    {
        Task<IEnumerable<Transfer>> GetLocalTransfers();
        Task<Transfer> AddTransfer(Transfer transfer);
        Task<Transfer> DeleteTransfer(Transfer transfer);
        Task<Transfer> EditTransfer(Transfer transfer);
    }
}
