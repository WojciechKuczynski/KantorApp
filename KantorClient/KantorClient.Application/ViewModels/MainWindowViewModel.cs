using KantorClient.Application.Consts;
using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Rates;
using KantorClient.BLL.Services.Interfaces;
using System.Windows;
using Prism.Commands;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        private readonly ISettingsService _settingService;

        #region MainViews
        public MainWindowView FormType { get; private set; }

        public IRatesMainViewModel RatesMainVM { get; private set; }
        #endregion
        public MainWindowViewModel(ISettingsService settingsService, IRatesMainViewModel ratesMainVM)
        {
            _settingService = settingsService;

            RatesMainVM = ratesMainVM;
            RatesMainVM.Parent = this;

            RatesMainViewCommand = new DelegateCommand(RatesMainView);
        }

        public Window Parent { get; set; }

        public bool LoggedOut { get; set; }

        public void Load()
        {
            var loaded = _settingService.LoadSettings();
        }

        #region Commands
        public ICommand RatesMainViewCommand { get; private set; }
        private void RatesMainView()
        {
            FormType = MainWindowView.Rates;
        }
        #endregion
    }
}
