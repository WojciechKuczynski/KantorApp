using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.Repositories.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;

namespace KantorClient.Application.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public readonly IMainWindowViewModel _mainWindowVM;
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfigurationRepository _configurationRepository;

        private bool _loginMode;

        public LoginView(IMainWindowViewModel mwVM, IAuthenticationService authenticationService, IConfigurationRepository configurationRepository, bool loginMode)
        {
            InitializeComponent();
            _mainWindowVM = mwVM;
            _authenticationService = authenticationService;
            _configurationRepository = configurationRepository;
            KantorTxt.Text = _configurationRepository.Kantor;
            _loginMode = loginMode;
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
                var loggedResponseArgs = await _authenticationService.LogIn(LoginTxt.Text, PasswordTxt.Password, KantorTxt.Text, false);
                if (loggedResponseArgs.Error)
                {
                    MessageBox.Show(loggedResponseArgs.ErrorMessage);
                    LoginButton.IsEnabled = true;
                    return;
                }

                if (_loginMode)
                {
                    var mainWindow = new MainWindow();
                    _mainWindowVM.Parent = mainWindow;
                    await _mainWindowVM.Load();
                    mainWindow.DataContext = _mainWindowVM;
                    _mainWindowVM.LoggedOut = false;
                    this.Hide();
                    mainWindow.ShowDialog();
                }
                else
                {
                    this.Hide();
                }
                LoginTxt.Text = string.Empty;
                PasswordTxt.Password = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas logowania!" + "\n" + ex.GetBaseException());
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
