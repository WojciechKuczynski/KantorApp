using KantorClient.BLL.Models;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Users
{
    public interface IUsersPermissionAddEditViewModel
    {
        IUsersPermissionViewModel Parent { get; set; }
        Task Load(bool loaded);

        void LoadForm(UserPermissionModel model = null);
    }
}
