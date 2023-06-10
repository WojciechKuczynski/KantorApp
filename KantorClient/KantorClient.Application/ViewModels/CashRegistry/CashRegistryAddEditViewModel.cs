using KantorClient.Application.ViewModels.Interfaces.CashRegistry;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.CashRegistry
{
    public class CashRegistryAddEditViewModel : ICashRegistryAddEditViewModel, INotifyPropertyChanged
    {
        private readonly ISettingsService _settingsService;
        private bool isEdited;

        public ICashRegistryMainViewParent Parent { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public bool Loading { get; set; }
        public ObservableCollection<CurrencyModel> Currencies { get; set; }
        public CurrencyModel SelectedCurrency { get; set; }
        public decimal BeforeChangeQuantity { get; set; }

        public CashRegistryModel Model { get; set; }
        public CashRegistryAddEditViewModel(ICashRegistryMainViewParent parent, ISettingsService settingsService)
        {

            _settingsService = settingsService;

            Parent = parent;
            AddCommand = new DelegateCommand(Add);
            CancelCommand = new DelegateCommand(Cancel);

        }
        public Task Load(bool loaded)
        {
            Currencies = new ObservableCollection<CurrencyModel>(_settingsService.Currencies.Select(x => new CurrencyModel(x)));
            return Task.CompletedTask;
        }

        public void LoadForm(CashRegistryModel model = null)
        {
            Model = model?.Clone();
            if (Model == null)
            {
                Model = new CashRegistryModel();
                isEdited = false;
            }
            else
            {
                isEdited = true;
                SelectedCurrency = Currencies.FirstOrDefault(x => x.Symbol == Model.Currency.Symbol);
                BeforeChangeQuantity = Model.Quantity;
            }
        }
        public ICommand AddCommand { get; private set; }
        private void Add()
        {
            try
            {
                Loading = true;
                if (isEdited && Model.Currency.Symbol == SelectedCurrency.Symbol)
                {
                    Edit();
                    Loading = false;
                    return;
                }
                if (SelectedCurrency == null)
                {
                    MessageBox.Show("Nie wybrano żadnej waluty");
                }
                else
                {
                    Model.Id = 0; // If someone on edit changed Currency
                    Model.Currency = SelectedCurrency;
                    Parent.AddRegistry(Model).GetAwaiter();
                }
            }
            finally
            {
                Loading = false;
            }
        }
        private void Edit()
        {
            Parent.EditRegistry(Model).GetAwaiter();
        }

        public ICommand CancelCommand { get; private set; }
        private void Cancel()
        {
            try
            {
                Loading = true;
                if (Model.Quantity != BeforeChangeQuantity)
                {
                    Model.Quantity = BeforeChangeQuantity;
                }

                Parent.CancelAddEditWindow();
            }
            finally
            {
                Loading = false;
            }
        }
    }
}
