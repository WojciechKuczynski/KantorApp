using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KantorClient.Application.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public readonly IMainWindowViewModel _mainWindowVM;
        private readonly IAuthenticationService _authenticationService;
        private readonly ISynchronizationService _synchronizationService;

        public LoginView(IMainWindowViewModel mwVM, IAuthenticationService authenticationService, ISynchronizationService synchronizationService)
        {
            InitializeComponent();
            _mainWindowVM = mwVM;
            _authenticationService = authenticationService;
            _synchronizationService = synchronizationService;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginButton.IsEnabled = false;
                var loggedIn = await _authenticationService.LogIn(LoginTxt.Text, PasswordTxt.Password);
                if (!loggedIn)
                {
                    MessageBox.Show("Niepoprawny użytkownik lub hasło!");
                    LoginButton.IsEnabled = true;
                    return;
                }

                var mainWindow = new MainWindow();
                _mainWindowVM.Parent = mainWindow;
                await _mainWindowVM.Load();
                mainWindow.DataContext = _mainWindowVM;
                LoginTxt.Text = string.Empty;
                PasswordTxt.Password = string.Empty;
                _synchronizationService.StartSynchronization();
                this.Hide();
                mainWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Niepoprawny użytkownik lub hasło!");
            }
            LoginButton.IsEnabled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login(null, null);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            LoginTxt.Focus();
        }
    }
}
