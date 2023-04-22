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

        public LoginView(IMainWindowViewModel mwVM, IAuthenticationService authenticationService)
        {
            InitializeComponent();
            _mainWindowVM = mwVM;
            _authenticationService = authenticationService;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            try
            {
                var loggedIn = await _authenticationService.LogIn(LoginTxt.Text, PasswordTxt.Password);
                if (!loggedIn)
                {
                    MessageBox.Show("Niepoprawny użytkownik lub hasło!");
                    return;
                }

                var mainWindow = new MainWindow();
                _mainWindowVM.Parent = mainWindow;
                _mainWindowVM.Load();
                mainWindow.DataContext = _mainWindowVM;

                LoginTxt.Text = string.Empty;
                PasswordTxt.Password = string.Empty;

                this.Hide();
                mainWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Niepoprawny użytkownik lub hasło!");
            }
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
