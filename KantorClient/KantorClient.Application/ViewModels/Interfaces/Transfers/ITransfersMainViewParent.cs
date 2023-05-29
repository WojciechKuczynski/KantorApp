using KantorClient.BLL.Models;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Transfers
{
    public interface ITransfersMainViewParent
    {
        void CancelForm();
        Task<bool> AddTransfer(TransferModel transferModel);
        Task<bool> EditTransfer(TransferModel transferModel);
    }
}
