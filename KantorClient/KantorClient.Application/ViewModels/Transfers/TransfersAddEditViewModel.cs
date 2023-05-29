﻿using KantorClient.Application.ViewModels.Interfaces.Transfers;
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
        private readonly ISettingsService _settingsService;
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
        public bool Loading { get; set; }
        public ObservableCollection<CurrencyModel> Currencies { get; set; }
        public CurrencyModel SelectedCurrency { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public TransfersAddEditViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

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
            if(type.HasValue)
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
                    MessageBox.Show("Wybierz walutę!");
                    return;
                }

                if (Model.TransferValue < 0)
                {
                    MessageBox.Show("Wartość transferu nie może być mniejsza od zera!");
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
                    MessageBox.Show(string.Format("Nie udało się {0} transferu", NewTransaction ? "dodać" : "edytować"));
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