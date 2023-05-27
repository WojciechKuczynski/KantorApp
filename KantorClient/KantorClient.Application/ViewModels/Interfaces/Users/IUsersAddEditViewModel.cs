using KantorClient.Application.ViewModels.Interfaces.Rates;
using KantorClient.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Users
{
    public interface IUsersAddEditViewModel
    {
        IUsersMainViewParent Parent { get; set; }
        Task Load(bool loaded);

        void LoadForm(UserModel model = null);
    }
}
