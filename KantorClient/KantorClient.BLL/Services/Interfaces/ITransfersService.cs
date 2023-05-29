using KantorClient.BLL.Models;
using KantorClient.Model;

namespace KantorClient.BLL.Services.Interfaces
{
    public interface ITransfersService
    {
        Task<List<TransferModel>> GetLocalTransfers();
        Task<TransferModel> AddTransfer(TransferModel Transfer, UserSession userSession);
        Task<TransferModel> EditTransfer(TransferModel Transfer, UserSession userSession);
        Task<bool> DeleteTransfer(TransferModel Transfer);
    }
}
