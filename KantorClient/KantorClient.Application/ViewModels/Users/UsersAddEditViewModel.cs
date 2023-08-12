using KantorClient.Application.CustomControls;
using KantorClient.Application.ViewModels.Interfaces.Users;
using KantorClient.BLL.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Users
{
    public class UsersAddEditViewModel : IUsersAddEditViewModel, INotifyPropertyChanged
    {
        public IUsersEditMainViewModel Parent { get; set; }

        public bool Editing { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public UsersAddEditViewModel()
        {
            CancelCommand = new DelegateCommand(Cancel);
            SaveCommand = new DelegateCommand<string>(Save);
        }

        public Task Load(bool loaded)
        {
            return Task.CompletedTask;
        }

        public void LoadForm(UserModel model = null)
        {
            User = model;
            User ??= new UserModel();
            Editing = User.Id != 0;
            if (model.UserPermissionId > 0)
            {
                SelectedPermission = Permissions.FirstOrDefault(x => x.Id == model.UserPermissionId);
            }
            else
            {
                SelectedPermission = null;
            }
        }

        public UserModel User { get; set; }

        public ObservableCollection<UserPermissionModel> Permissions { get; set; }
        public UserPermissionModel SelectedPermission { get; set; }

        public ICommand CancelCommand { get; private set; }
        private void Cancel()
        {
            Parent.CancelAddEditWindow();
        }

        public ICommand SaveCommand { get; private set; }
        private void Save(string password)
        {
            if (SelectedPermission == null)
            {
                new UserMessageBox("Wybierz jakieś uprawnienie!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning).Show();
                return;
            }

            User.UserPermissionId = SelectedPermission.Id;

            if (!Editing)
            {
                User.Password = password;
                Parent.AddUser(User);
            }
            else
            {
                Parent.EditUser(User);
            }
        }

        public Task OnShow(List<UserPermissionModel> permissions)
        {
            Permissions = new ObservableCollection<UserPermissionModel>(permissions);
            return Task.CompletedTask;
        }
    }
}
