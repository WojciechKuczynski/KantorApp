using KantorClient.BLL.Models;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.CashRegistry
{
    public interface ICashRegistryMainViewParent
    {
        Task AddRegistry(CashRegistryModel model);
        void CancelAddEditWindow();
        Task EditRegistry(CashRegistryModel model);
        Task SetPln(decimal quantity);
    }
}
