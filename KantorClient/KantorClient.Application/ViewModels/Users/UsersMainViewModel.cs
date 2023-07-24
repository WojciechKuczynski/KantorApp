using KantorClient.Application.CustomControls;
using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Users;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using Prism.Commands.Ex;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Users
{
    public class UsersMainViewModel : IUsersMainViewModel, INotifyPropertyChanged
    {
        private readonly IUsersService _usersService;

        public UsersMainViewModel(IUsersService usersService)
        {
            _usersService = usersService;

            AddEditVM = new UsersAddEditViewModel();
            AddEditVM.Parent = this;

            AddUserCommand = new DelegateCommand(AddUser);
            RefreshCommand = new DelegateCommand(Refresh);
            EditUserCommand = new DelegateCommand(EditUser);
            RemoveUserCommand = new DelegateCommand<UserModel>(RemoveUser);
        }
        public IMainWindowContainer Parent { get; set; }
        public bool AddEnabled => !FormOpened;
        private bool _showDeleted;
        public bool EditEnabled => SelectedUser != null && !FormOpened;
        public bool FormOpened { get; set; }
        public bool Loading { get; set; }
        public bool ShowDeleted
        {
            get { return _showDeleted; }
            set
            {
                _showDeleted = value;
                RefreshUsersList();
            }
        }

        private void RefreshUsersList()
        {
            Users = new ObservableCollection<UserModel>(UserCollection.Where(x => x.Valid || ShowDeleted));
        }

        public IUsersAddEditViewModel AddEditVM { get; set; }
        public ObservableCollection<UserModel> Users { get; set; }
        private List<UserModel> UserCollection { get; set; }
        public UserModel SelectedUser { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task AddUser(UserModel model)
        {
            if (model != null)
            {
                var added = await _usersService.AddUser(model);
                if (added != null)
                {
                    Users.Add(added);
                    FormOpened = false;
                }
                else
                {
                    new UserMessageBox("Nie udało się dodać pracownika", MessageBoxButton.OK, MessageBoxImage.Error).ShowMessage();
                }
            }
        }

        public void CancelAddEditWindow()
        {
            FormOpened = false;
        }

        public async Task EditUser(UserModel model)
        {
            try
            {
                if (model != null)
                {
                    var edited = await _usersService.EditUser(model);
                    if (edited == null)
                    {
                        return;
                    }
                    var old = Users.FirstOrDefault(x => x == model);
                    old = edited;
                    FormOpened = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task Load(bool loaded = false)
        {
            var users = await _usersService.GetUsers();
            Users = new ObservableCollection<UserModel>(users);
            await AddEditVM.Load(loaded);
        }

        #region Commands

        public ICommand AddUserCommand { get; private set; }
        private void AddUser()
        {
            AddEditVM.LoadForm(SelectedUser);
            FormOpened = true;
        }

        public ICommand EditUserCommand { get; private set; }
        private void EditUser()
        {
            AddEditVM.LoadForm(SelectedUser);
            FormOpened = true;
        }

        public ICommand RefreshCommand { get; private set; }
        private async void Refresh()
        {
            try
            {
                Loading = true;
                var users = await _usersService.GetUsers();
                UserCollection = new List<UserModel>(users);
                RefreshUsersList();
            }
            finally
            {
                Loading = false;
            }
        }

        public ICommand RemoveUserCommand { get; private set; }
        private async void RemoveUser(UserModel model)
        {
            try
            {
                if (model != null && model.Valid == true)
                {
                    model.Valid = false;
                    var edited = await _usersService.EditUser(model);
                    if (edited == null)
                    {
                        return;
                    }
                    var old = Users.FirstOrDefault(x => x == model);
                    old = edited;
                    FormOpened = false;
                    Refresh();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Task OnShow()
        {
            Refresh();
            return Task.CompletedTask;
        }

        #endregion
    }
}
