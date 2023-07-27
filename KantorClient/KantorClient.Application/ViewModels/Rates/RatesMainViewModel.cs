using KantorClient.Application.CustomControls;
using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Rates;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorServer.Model.Consts;
using Prism.Commands.Ex;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KantorClient.Common.Extentions;

namespace KantorClient.Application.ViewModels.Rates
{
    public class RatesMainViewModel : IRatesMainViewModel, INotifyPropertyChanged
    {
        #region fields
        private readonly ISettingsService _settingsService;
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Properties
        public IMainWindowContainer Parent { get; set; }
        public IRatesAddEditViewModel AddEditVM { get; set; }

        public bool AddEnabled => CanAdd && !FormOpened;
        public bool EditEnabled => CanEdit && SelectedRate != null && !FormOpened;
        public bool FormOpened { get; set; }
        public bool Loading { get; set; }

        // Permissions
        private bool CanAdd { get; set; }
        private bool CanEdit { get; set; }
        public bool CanDelete { get; private set; }

        public ObservableCollection<RateModel> Rates { get; set; }
        public RateModel SelectedRate { get; set; }

        #endregion
        public RatesMainViewModel(ISettingsService settingsService, IRatesAddEditViewModel ratesAddEditVM, IAuthenticationService authenticationService)
        {
            _settingsService = settingsService;
            _authenticationService = authenticationService;
            AddEditVM = ratesAddEditVM;
            AddRateCommand = new DelegateCommand(AddRate);
            EditRateCommand = new DelegateCommand(EditRate);
            RemoveRateCommand = new DelegateCommand<RateModel>(RemoveRate);
            RefreshCommand = new DelegateCommand(Refresh);
        }

        #region Methods

        public async Task Load(bool loaded)
        {
            if (loaded)
            {
                Rates = new ObservableCollection<RateModel>(_settingsService.Rates.Select(x => new RateModel(x)));
            }
            await AddEditVM.Load(loaded);
            AddEditVM.Parent = this;

            CanAdd = _authenticationService.UserSession.HasUserPermission(PermissionKeys.Rate.AddRate);
            CanEdit = _authenticationService.UserSession.HasUserPermission(PermissionKeys.Rate.EditRate);
            CanDelete = _authenticationService.UserSession.HasUserPermission(PermissionKeys.Rate.DeleteRate);
        }

        public void CancelAddEditWindow()
        {
            FormOpened = false;
        }

        public async void AddRate(RateModel rateModel)
        {
            if (rateModel != null)
            {
                var rate = await _settingsService.AddRate(RateModel.Map(rateModel));
                Rates.Add(new RateModel(rate));
                FormOpened = false;
            }
        }

        public async void EditRate(RateModel rateModel)
        {
            if (rateModel != null)
            {
                var rate = await _settingsService.EditRate(RateModel.Map(rateModel));
                var rateInList = Rates.FirstOrDefault(x => x.Id == rate.Id);
                rateInList = new RateModel(rate);
                FormOpened = false;
            }
        }

        public Task OnShow()
        {
            Refresh();
            return Task.CompletedTask;
        }

        #endregion

        #region Commands

        public ICommand AddRateCommand { get; private set; }
        private void AddRate()
        {
            AddEditVM.LoadForm();
            FormOpened = true;
        }

        public ICommand EditRateCommand { get; private set; }
        private void EditRate()
        {
            AddEditVM.LoadForm(SelectedRate);
            FormOpened = true;
        }

        public ICommand RemoveRateCommand { get; private set; }
        private async void RemoveRate(RateModel model)
        {
            if (model != null)
            {
                var option = new UserMessageBox("Czy chcesz usunąć ten Kurs?",  MessageBoxButton.YesNo,MessageBoxImage.Question).ShowMessage();
                if (!option)
                {
                    return;
                }

                var success = await _settingsService.RemoveRate(RateModel.Map(model));
                if (!success)
                {
                    new UserMessageBox("Nie udało się usunąć kursu", MessageBoxButton.OK, MessageBoxImage.Error).ShowDialog();
                }
                else
                {
                    var deleted = Rates.FirstOrDefault(x => x == model);
                    deleted.Valid = false;
                }
            }
        }

        public ICommand RefreshCommand { get; private set; }
        private void Refresh()
        {
            try
            {
                Loading = true;
                var rates = _settingsService?.Rates?.Select(x => new RateModel(x));
                if (rates != null)
                {
                    Rates = new ObservableCollection<RateModel>(rates);
                }
            }
            finally
            {
                Loading = false;
            }
        }

        #endregion
    }
}
