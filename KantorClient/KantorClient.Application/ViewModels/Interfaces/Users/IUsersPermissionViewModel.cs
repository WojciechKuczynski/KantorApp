using KantorClient.BLL.Models;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Users
{
    public interface IUsersPermissionViewModel
    {
        Task AddUserPermission(UserPermissionModel userPermission);
        void CancelAddEditWindow();
        Task EditUserPermission(UserPermissionModel userPermission);
        Task Load(bool loaded = false);
        Task OnShow();
    }
}
