using KantorClient.BLL.Models;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Users
{
    public interface IUsersEditMainViewModel
    {
        Task AddUser(UserModel model);
        void CancelAddEditWindow();
        Task EditUser(UserModel model);

        Task Load(bool loaded = false);
        Task OnShow();
    }
}
