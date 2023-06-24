using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.Views;
using KantorClient.BLL;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL;
using KantorClient.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace KantorClient.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private readonly Modules _modules = new Modules();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _modules.SetDependencies(new ApplicationModule());
            _modules.Verify();

            var dataContext = _modules.Container.GetInstance<DataContext>();
            if (dataContext.Database.GetPendingMigrations().Any())
            {
                dataContext.Database.Migrate();
            }

            var mVM = _modules.Container.GetInstance<IMainWindowViewModel>();
            var authenticationService = _modules.Container.GetInstance<IAuthenticationService>();
            var login = new LoginView(mVM, authenticationService, true);
            do
            {
                login.ShowDialog();
            }
            while (login._mainWindowVM.LoggedOut);
            login.Close();
        }
    }
}
