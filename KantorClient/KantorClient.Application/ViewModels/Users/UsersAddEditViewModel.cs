using KantorClient.Application.ViewModels.Interfaces.Users;
using KantorClient.BLL.Models;
using KantorClient.Model.Consts;
using Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Users
{
    public class UsersAddEditViewModel : IUsersAddEditViewModel, INotifyPropertyChanged
    {
        private KeyValuePair<UserPermission, string> _selectedPermission;

        public IUsersMainViewParent Parent { get; set; }

        public bool Editing { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public UsersAddEditViewModel()
        {
            CancelCommand = new DelegateCommand(Cancel);
            SaveCommand = new DelegateCommand<string>(Save);
        }

        public KeyValuePair<UserPermission, string> SelectedPermission
        {
            get { return _selectedPermission; }
            set
            {
                _selectedPermission = value;
                if (User != null)
                {
                    User.Permission = value.Key;
                }
            }
        }

        public Dictionary<UserPermission, string> Permissions 
            => new Dictionary<UserPermission, string> { { UserPermission.Employee, "Kasjer" },
                                                        { UserPermission.Manager, "Menedżer" },
                                                        { UserPermission.Admin, "Admin" }}; 
        public Task Load(bool loaded)
        {
            return Task.CompletedTask;
        }

        public void LoadForm(UserModel model = null)
        {
            SelectedPermission = Permissions.FirstOrDefault(x => x.Key == model?.Permission);
            User = model;
            User ??= new UserModel();
            Editing = User.Id != 0;
        }

        public UserModel User { get; set; }

        public ICommand CancelCommand { get; private set; }
        private void Cancel()
        {
            Parent.CancelAddEditWindow();
        }

        public ICommand SaveCommand { get; private set; }
        private void Save(string password)
        {
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
    }
}
