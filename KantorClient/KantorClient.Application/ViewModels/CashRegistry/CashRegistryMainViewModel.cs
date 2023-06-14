using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.CashRegistry;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.CashRegistry
{
    public class CashRegistryMainViewModel : ICashRegistryMainViewModel, INotifyPropertyChanged
    {

        private readonly ICashRegistryService _cashRegistryService;
        private readonly IAuthenticationService _authenticationService;
        public IMainWindowContainer Parent { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<CashRegistryModel> Registries { get; set; }
        public CashRegistryModel SelectedRegistry { get; set; }
        public ICashRegistryAddEditViewModel AddEditVM { get; set; }
        public ICashRegistryPlnViewModel SetPlnVM { get; set; }
        public bool AddEnabled => !FormOpened;
        public bool EditEnabled => SelectedRegistry != null && !FormOpened; 
        public bool SetPlnVisible { get; set; }
        public bool FormOpened { get; set; }
        public bool EditMode => FormOpened || SetPlnVisible;

        public CashRegistryMainViewModel(ISettingsService settingsService, ICashRegistryService cashRegistryService, IAuthenticationService authenticationService)
        {
            FormOpened = false;

            AddRegistryCommand = new DelegateCommand(AddCashRegistry);
            RemoveRegistryCommand = new DelegateCommand<CashRegistryModel>(RemoveRegistry);
            RefreshCommand = new DelegateCommand(Refresh);
            SetPLNCommand = new DelegateCommand(SetPLN);
            EditRegistryCommand = new DelegateCommand(EditCashRegistry);

            AddEditVM = new CashRegistryAddEditViewModel(this, settingsService);
            SetPlnVM = new CashRegistryPlnViewModel(this);
            _cashRegistryService = cashRegistryService;
            _authenticationService = authenticationService;
        }

        public async Task AddRegistry(CashRegistryModel model)
        {
            if (model != null)
            {
                var newRegistry = await _cashRegistryService.AddRegistry(model);
                if (newRegistry != null)
                {
                    Registries.Add(newRegistry);
                }
            }
            CancelAddEditWindow();
        }

        public void CancelAddEditWindow()
        {
            FormOpened = false;
        }

        public async Task EditRegistry(CashRegistryModel model)
        {
            if (model != null)
            {
                var editedRegistry = await _cashRegistryService.EditRegistry(model);
                if (editedRegistry == null)
                {
                    return;
                }

                if (editedRegistry.Id != model.Id)
                {
                    Registries.Add(editedRegistry);
                }
                else
                {
                    var registryInCollection = Registries.FirstOrDefault(x => x.Id == editedRegistry.Id);
                    if (registryInCollection != null)
                    {
                        registryInCollection.Quantity = editedRegistry.Quantity;
                    }
                }
            }

            CancelAddEditWindow();
        }

        public async Task Load(bool loaded = false)
        {
            var registries = await _cashRegistryService.GetRegistries();
            Registries = new ObservableCollection<CashRegistryModel>(registries);
            await AddEditVM.Load(loaded);
        }

        public ICommand AddRegistryCommand { get; private set; }
        private void AddCashRegistry()
        {
            AddEditVM.LoadForm();
            FormOpened = true;
        }

        public ICommand EditRegistryCommand { get; private set; }
        private void EditCashRegistry()
        {
            AddEditVM.LoadForm(SelectedRegistry);
            FormOpened = true;
        }


        public ICommand RemoveRegistryCommand { get; private set; }
        private void RemoveRegistry(CashRegistryModel model)
        {
            _cashRegistryService.DeleteRegistry(model);
        }

        public ICommand RefreshCommand { get; private set; }
        private async void Refresh()
        {
            var registries = await _cashRegistryService.GetRegistries();
            Registries = new ObservableCollection<CashRegistryModel>(registries);
        }

        public ICommand SetPLNCommand { get; private set; }
        private void SetPLN()
        {
            SetPlnVisible = true;
            SetPlnVM.LoadForm();
        }

        public Task SetPln(decimal quantity)
        {
            var changed = _authenticationService.SetPln(quantity).GetAwaiter();
            if (changed.GetResult())
            {
                
            }
            SetPlnVisible = false;
            return Task.CompletedTask;
        }
    }
}
