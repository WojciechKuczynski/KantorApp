using KantorClient.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Users
{
    public interface IUsersMainViewParent
    {
        Task AddUser(UserModel model);
        void CancelAddEditWindow();
        Task EditUser(UserModel model);
    }
}
