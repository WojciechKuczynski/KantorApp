using KantorClient.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Users
{
    public interface IUsersAddEditViewModel
    {
        IUsersEditMainViewModel Parent { get; set; }
        Task Load(bool loaded);

        void LoadForm(UserModel model = null);
        Task OnShow(List<UserPermissionModel> permissions);
    }
}
