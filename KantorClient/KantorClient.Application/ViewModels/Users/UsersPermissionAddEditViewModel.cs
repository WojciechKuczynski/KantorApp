using KantorClient.Application.ViewModels.Interfaces.Users;
using KantorClient.BLL.Models;
using Prism.Commands.Ex;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Users
{
    public class UsersPermissionAddEditViewModel : IUsersPermissionAddEditViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        #region Properties

        public string Header { get; set; }

        public UserPermissionModel UserPermission { get; set; }
        public IUsersPermissionViewModel Parent { get; set; }

        #endregion

        public UsersPermissionAddEditViewModel()
        {
            CancelCommand = new DelegateCommand(Cancel);
            SaveCommand = new DelegateCommand(Save);
        }

        #region Methods 

        public Task Load(bool loaded)
        {
            return Task.CompletedTask;
        }

        public void LoadForm(UserPermissionModel model = null)
        {
            Header = model == null ? "Dodaj uprawnienie" : "Edytuj uprawnienie";

            UserPermission = model ?? new UserPermissionModel();
        }

        #endregion

        #region Command

        public ICommand SaveCommand { get; private set; }
        private void Save()
        {
            if (UserPermission.Id == 0)
            {
                Parent.AddUserPermission(UserPermission);
            }
            else
            {
                Parent.EditUserPermission(UserPermission);
            }
        }

        public ICommand CancelCommand { get; private set; }
        private void Cancel()
        {
            Parent.CancelAddEditWindow();
        }

        #endregion
    }
}
