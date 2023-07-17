using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Reports
{
    public interface IReportsTransactionsViewModel
    {
        Task Load(bool loaded = false);
        Task OnShow();
    }
}
