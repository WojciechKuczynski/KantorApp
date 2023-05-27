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
using System.Text;
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
        }
        public IMainWindowContainer Parent { get; set; }
        public bool AddEditVisible { get; set; }
        public IUsersAddEditViewModel AddEditVM { get; set; }
        public ObservableCollection<UserModel> Users { get; set; }
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
                    AddEditVisible = false;
                }
                else
                {
                    MessageBox.Show("Nie udało się dodać pracownika");
                }
            }
        }

        public void CancelAddEditWindow()
        {
            AddEditVisible = false;
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
                    AddEditVisible = false;
                }
            }
            catch(Exception ex)
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
            AddEditVisible = true;
        }

        #endregion
    }
}
