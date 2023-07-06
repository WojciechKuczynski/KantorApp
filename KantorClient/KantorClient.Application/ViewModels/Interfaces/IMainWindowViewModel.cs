using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KantorClient.Application.ViewModels.Interfaces
{
    public interface IMainWindowViewModel : IMainWindowContainer
    {
        Window Parent { get; set; }
        bool LoggedOut { get; set; }
        Task Load();
        //UserSession LoggedUser { get; set; }

        //void LoadUser(User user);
    }
}
