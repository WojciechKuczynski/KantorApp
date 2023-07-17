using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Reports
{
    public interface IReportsUsersViewModel
    {
        Task Load(bool loaded = false);
    }
}
