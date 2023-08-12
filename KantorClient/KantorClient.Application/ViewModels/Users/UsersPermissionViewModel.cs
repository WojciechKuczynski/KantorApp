using KantorClient.Application.CustomControls;
using KantorClient.Application.ViewModels.Interfaces.Users;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using Prism.Commands.Ex;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Users
{
    public class UsersPermissionViewModel : IUsersPermissionViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        #region fields

        private readonly IUsersService _usersService;

        private UserPermissionModel _selectedUserPermission;
        private ObservableCollection<PermissionModel> _permissions;

        #endregion

        #region Properties

        public bool Loading { get; set; }
        public bool FormOpened { get; set; }

        public IUsersPermissionAddEditViewModel AddEditVM { get; set; }

        public ObservableCollection<UserPermissionModel> UserPermissions { get; set; }
        public ObservableCollection<PermissionModel> Permissions
        {
            get => _permissions;
            set
            {
                _permissions = value;
            }
        }

        private List<UserPermissionModel> UserPermissionCollection { get; set; }
        private List<PermissionModel> PermissionsCollection { get; set; }

        public UserPermissionModel SelectedUserPermission
        {
            get => _selectedUserPermission;
            set
            {
                _selectedUserPermission = value;
                if (value != null)
                {
                    RefreshPermissions(value);
                }
            }
        }

        #endregion

        public UsersPermissionViewModel(IUsersService usersService)
        {
            _usersService = usersService;
            Permissions = new ObservableCollection<PermissionModel>();
            UserPermissions = new ObservableCollection<UserPermissionModel>();
            UserPermissionCollection = new List<UserPermissionModel>();
            PermissionsCollection = new List<PermissionModel>();

            RefreshCommand = new DelegateCommand(Refresh);
            AddUserPermissionCommand = new DelegateCommand(AddUserPermission);
            SavePermissionsCommand = new DelegateCommand(SavePermissions);
            SelectPermissionCommand = new DelegateCommand<PermissionModel>(SelectPermission);
            EditUserPermissionCommand = new DelegateCommand(EditUserPermission);

            AddEditVM = new UsersPermissionAddEditViewModel();
            AddEditVM.Parent = this;
        }


        #region Methods

        public async Task Load(bool loaded = false)
        {
            if (loaded)
            {
                var userPermissions = await _usersService.GetUserPermissions();
                UserPermissionCollection = new List<UserPermissionModel>(userPermissions);

                var permissions = await _usersService.GetPermissions();
                PermissionsCollection = new List<PermissionModel>(permissions);
            }
        }

        public Task OnShow()
        {
            UserPermissions = new ObservableCollection<UserPermissionModel>(UserPermissionCollection);
            return Task.CompletedTask;
        }

        private void RefreshPermissions(UserPermissionModel value)
        {
            Permissions = new ObservableCollection<PermissionModel>(PermissionsCollection);

            foreach (var perm in Permissions)
            {
                if (value.Permissions.Any(x => x.Id == perm.Id))
                {
                    perm.ActiveInPermission = true;
                }
                else
                {
                    perm.ActiveInPermission = false;
                }
            }
            //Permissions = new ObservableCollection<PermissionModel>(Permissions);
        }

        public async Task AddUserPermission(UserPermissionModel userPermission)
        {
            if (userPermission != null)
            {
                UserPermissionModel up = await _usersService.AddUserPermission(userPermission);
                if (up != null)
                {
                    Refresh();
                    CancelAddEditWindow();
                }
                else
                {
                    new UserMessageBox("Wystąpił bład podczas dodawania uprawnienia", MessageBoxButton.OK, MessageBoxImage.Error).ShowDialog();
                }
            }
        }

        public async Task EditUserPermission(UserPermissionModel userPermission)
        {
            if (userPermission != null)
            {
                UserPermissionModel up = await _usersService.EditUserPermission(userPermission);
                if (up != null)
                {
                    Refresh();
                    CancelAddEditWindow();
                }
                new UserMessageBox("Wystąpił bład podczas edycji uprawnienia", MessageBoxButton.OK, MessageBoxImage.Error).ShowDialog();
            }
        }

        public void CancelAddEditWindow()
        {
            this.FormOpened = false;
        }

        #endregion

        #region Commands

        public ICommand SelectPermissionCommand { get; private set; }
        private void SelectPermission(PermissionModel model)
        {
            if (model != null)
            {
                model.ActiveInPermission = !model.ActiveInPermission;
                Permissions = new ObservableCollection<PermissionModel>(Permissions);
            }
        }

        public ICommand RefreshCommand { get; private set; }
        private async void Refresh()
        {
            var userPermissions = await _usersService.GetUserPermissions();
            UserPermissionCollection = new List<UserPermissionModel>(userPermissions);
            UserPermissions = new ObservableCollection<UserPermissionModel>(UserPermissionCollection);

            var permissions = await _usersService.GetPermissions();
            PermissionsCollection = new List<PermissionModel>(permissions);
        }

        public ICommand AddUserPermissionCommand { get; private set; }
        private void AddUserPermission()
        {
            AddEditVM.LoadForm();
            FormOpened = true;
        }

        public ICommand EditUserPermissionCommand { get; private set; }
        private void EditUserPermission()
        {
            if (SelectedUserPermission != null)
            {
                AddEditVM.LoadForm(SelectedUserPermission);
                FormOpened = true;
            }
        }

        public ICommand SavePermissionsCommand { get; private set; }
        private async void SavePermissions()
        {
            if (SelectedUserPermission != null)
            {
               if (await _usersService.SavePermissionsToUserPermission(SelectedUserPermission, Permissions.Where(x => x.ActiveInPermission).ToList()))
                {
                    Refresh();
                }
            }
        }

        #endregion
    }
}
