using KantorClient.BLL.Models;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Transfers
{
    public interface ITransfersAddEditViewModel
    {
        ITransfersMainViewParent Parent { get; set; }
        Task Load();
        void LoadForm(TransferModel model = null);
    }
}
