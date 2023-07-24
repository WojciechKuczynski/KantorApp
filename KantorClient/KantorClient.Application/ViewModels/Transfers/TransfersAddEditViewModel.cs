using KantorClient.Application.CustomControls;
using KantorClient.Application.ViewModels.Interfaces.Transfers;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.Model.Consts;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Transfers
{
    public class TransfersAddEditViewModel : ITransfersAddEditViewModel, INotifyPropertyChanged
    {

        private TransferType _selectedType;
        private CurrencyModel _selectedCurrency;
        private readonly ISettingsService _settingsService;
        private readonly ICashRegistryService _cashRegistryService;

        public ITransfersMainViewParent Parent { get; set; }

        public TransferModel Model { get; set; }
        public string TransferText { get; set; }
        public TransferType SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                TransferText = value == TransferType.TransferIn ? "WPŁATA" : "WYPŁATA";
            }
        }
        public string AcceptTitle => NewTransaction ? "DODAJ" : "EDYTUJ";
        public bool NewTransaction { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrencyAmount { get; set; }
        public bool Loading { get; set; }
        public ObservableCollection<CurrencyModel> Currencies { get; set; }
        public CurrencyModel SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                _selectedCurrency = value;
                if (value != null)
                {
                    Task.Run(async () => { CurrencyAmount = await _cashRegistryService.GetAmountForCurrency(value); });
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public TransfersAddEditViewModel(ISettingsService settingsService, ICashRegistryService cashRegistryService)
        {
            _settingsService = settingsService;
            _cashRegistryService = cashRegistryService;

            AddCommand = new DelegateCommand(Add);
            CancelCommand = new DelegateCommand(Cancel);
            SelectTypeCommand = new DelegateCommand<TransferType?>(SelectType);
        }

        public Task Load()
        {
            Currencies = new ObservableCollection<CurrencyModel>(_settingsService.Currencies.Select(x => new CurrencyModel(x)));
            return Task.CompletedTask;
        }

        public void LoadForm(TransferModel model = null)
        {
            if (model == null)
            {
                Model = new TransferModel();
                NewTransaction = true;
                SelectedType = TransferType.TransferOut;
            }
            else
            {
                Model = model;
                NewTransaction = false;
                SelectedType = model.Type;
                SelectedCurrency = Currencies.FirstOrDefault(x => x.Symbol == model.TransferCurrency.Symbol);
            }
        }

        public ICommand SelectTypeCommand { get; private set; }
        private void SelectType(TransferType? type)
        {
            if (type.HasValue)
            {
                SelectedType = type.Value;
            }
        }

        public ICommand AddCommand { get; private set; }
        private async void Add()
        {
            if (Loading)
            {
                return;
            }
            try
            {
                Loading = true;

                if (SelectedCurrency == null)
                {
                    new UserMessageBox("Wybierz walutę!", MessageBoxButton.OK, MessageBoxImage.Warning).ShowMessage();
                    return;
                }

                if (Model.TransferValue <= 0)
                {
                    new UserMessageBox("Wartość transferu nie może być mniejsza od zera!", MessageBoxButton.OK, MessageBoxImage.Warning).ShowMessage();
                    return;
                }

                if (Model.TransferValue > CurrencyAmount && Model.Type == TransferType.TransferOut)
                {
                    new UserMessageBox("Na stanie nie ma tyle waluty!", MessageBoxButton.OK, MessageBoxImage.Warning).ShowMessage();
                    return;
                }
                // if TransferValue < CurrencyBalance ( for type == Out )

                Model.TransferCurrency = SelectedCurrency;
                Model.Type = SelectedType;
                var success = false;
                if (NewTransaction)
                {
                    success = await Parent.AddTransfer(Model);

                }
                else
                {
                    success = await Parent.EditTransfer(Model);
                }

                if (success)
                {
                    Parent.CancelForm();
                }
                else
                {
                    new UserMessageBox(string.Format("Nie udało się {0} transferu", NewTransaction ? "dodać" : "edytować"), MessageBoxButton.OK, MessageBoxImage.Error).ShowMessage();
                }
            }
            finally
            {
                Loading = false;
            }
        }

        public ICommand CancelCommand { get; private set; }
        private void Cancel()
        {
            SelectedCurrency = null;
            Parent.CancelForm();
        }
    }
}
