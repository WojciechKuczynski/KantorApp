using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Users;
using KantorClient.BLL.Services.Interfaces;
using Prism.Commands.Ex;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Users
{
    public class UsersMainViewModel : IUsersMainViewModel, INotifyPropertyChanged
    {
        public UsersMainViewModel(IUsersService usersService)
        {
            EditMainVM = new UsersEditMainViewModel(usersService);
            UsersPermissionMainVM = new UsersPermissionViewModel(usersService);

            UsersEditVisible = true;
            UsersEditCommand = new DelegateCommand(UsersEdit);
            UsersPermissionCommand = new DelegateCommand(UsersPermission);
        }
        public IMainWindowContainer Parent { get; set; }

        public IUsersEditMainViewModel EditMainVM { get; private set; }
        public IUsersPermissionViewModel UsersPermissionMainVM { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task Load(bool loaded = false)
        {
            await EditMainVM.Load(loaded);
            await UsersPermissionMainVM.Load(loaded);
        }

        public async Task OnShow()
        {
            await EditMainVM.OnShow();
            await UsersPermissionMainVM.OnShow();
        }

        public ICommand UsersEditCommand { get; private set; }
        public bool UsersEditVisible { get; private set; }
        public bool UsersPermissionVisible { get; private set; }

        private void UsersEdit()
        {
            UsersEditVisible = true;
            UsersPermissionVisible = false;
        }

        public ICommand UsersPermissionCommand { get; private set; }
        private void UsersPermission()
        {
            UsersPermissionVisible = true;
            UsersEditVisible = false;
        }
    }
}
