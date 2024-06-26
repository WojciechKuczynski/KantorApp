﻿using KantorClient.Application.CustomControls;
using KantorClient.Application.ViewModels.Interfaces.Transactions;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.Model.Consts;
using Prism.Commands.Ex;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Transactions
{
    public class TransactionsAddEditViewModel : ITransactionsAddEditViewModel, INotifyPropertyChanged
    {

        #region variables

        private decimal _finalValue;
        private decimal _amount;
        private decimal _changeRate;
        private decimal _cashValue;
        private bool _locker;

        private CurrencyModel _selectedCurrency;
        private RateModel _selectedRate;
        private TransactionType _selectedType;

        #endregion


        private readonly ISettingsService _settingsService;
        private readonly ICashRegistryService _cashRegistryService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public TransactionsAddEditViewModel(ISettingsService settingsService, ICashRegistryService cashRegistryService)
        {
            _settingsService = settingsService;
            _cashRegistryService = cashRegistryService;

            SelectedType = TransactionType.Sell;

            AddCommand = new DelegateCommand(Add);
            CancelCommand = new DelegateCommand(Cancel);
            SellCommand = new DelegateCommand(SelectSell);
            BuyCommand = new DelegateCommand(SelectBuy);
        }

        #region Properties

        public string AcceptTile => NewTransaction ? "DODAJ" : "EDYTUJ";

        public bool Loading { get; set; }
        public decimal NbpRate { get; set; }

        public bool NewTransaction { get; set; }

        public decimal CurrencyAmount { get; set; }
        public decimal ChangeValue { get; set; }
        public decimal CashValue
        {
            get { return _cashValue; }
            set
            {
                _cashValue = value;
                ChangeValue = value - FinalValue;
            }
        }

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                if (!_locker)
                {
                    _locker = true;
                    FinalValue = _changeRate * value;
                    _locker = false;
                }
                _amount = value;
            }
        }

        public decimal FinalValue
        {
            get { return _finalValue; }
            set
            {
                if (!_locker)
                {
                    _locker = true;
                    if (SelectedType == TransactionType.Sell)
                    {
                        Amount = Math.Round(value / _changeRate, MidpointRounding.ToNegativeInfinity);
                    }
                    else
                    {
                        Amount = Math.Round(value / _changeRate, MidpointRounding.ToPositiveInfinity);
                    }
                    //ChangeRate = Math.Round(value / _amount,2);
                    _locker = false;
                }
                _finalValue = value;
            }
        }

        public decimal ChangeRate
        {
            get { return _changeRate; }
            set
            {
                if (!_locker)
                {
                    _locker = true;
                    FinalValue = value * _amount;
                    _locker = false;
                }
                _changeRate = value;
            }
        }

        public string TransactionText => SelectedType == TransactionType.Sell ? "TRANSAKCJA SPRZEDAŻY" : "TRANSAKCJA KUPNA";

        public CurrencyModel SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                _selectedCurrency = value;
                if (value != null)
                {
                    AssignRateForCurrency(value);
                }
            }
        }

        public ObservableCollection<CurrencyModel> Currencies { get; set; }

        public ITransactionsMainViewParent Parent { get; set; }

        public TransactionModel Transaction { get; set; }

        public RateModel SelectedRate
        {
            get { return _selectedRate; }
            set
            {
                _selectedRate = value;
                if (value != null)
                {
                    ChangeRate = SelectedType == TransactionType.Sell ? Math.Round(value.DefaultSellRate,2) : Math.Round(value.DefaultBuyRate,2);
                }
            }
        }

        public TransactionType SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                if (SelectedRate != null)
                {
                    ChangeRate = value == TransactionType.Sell ? Math.Round(SelectedRate.DefaultSellRate,2) : Math.Round(SelectedRate.DefaultBuyRate,2);
                }
            }
        }

        #endregion

        public Task Load()
        {
            Currencies = new ObservableCollection<CurrencyModel>(_settingsService.Currencies.Select(x => new CurrencyModel(x)));
            return Task.CompletedTask;
        }

        public void LoadForm(TransactionModel model = null, TransactionType transactionType = TransactionType.Sell)
        {
            LoadModel(model,transactionType);
        }

        private void LoadModel(TransactionModel model, TransactionType transactionType)
        {
            if (model != null)
            {
                NewTransaction = false;
                SelectedCurrency = Currencies.FirstOrDefault(x => x.Symbol == model.Currency.Symbol);
                Transaction = model;
                _locker = true;
                ChangeRate = Math.Round(model.Rate,2);
                FinalValue = model.FinalValue;
                Amount = model.Quantity;
                SelectedType = model.TransactionType;
                _locker = false;
            }
            else
            {
                NewTransaction = true;
                SelectedCurrency = null;
                SelectedType = transactionType;
                SelectedRate = null;
                NbpRate = 0;
                CurrencyAmount = 0;
                Amount = 0;
                ChangeRate = 0;
                FinalValue = 0;
                CashValue = 0;
                ChangeValue = 0;
            }
        }

        private async Task AssignRateForCurrency(CurrencyModel currency)
        {
            var rate = _settingsService.Rates.FirstOrDefault(x => x.Currency.Id == currency.Id);
            var nbpRate = _settingsService.NbpRates?.FirstOrDefault(x => x.Currency.Id == currency.Id);

            if (rate == null)
            {
                new UserMessageBox("Nie ma kursów dla tej waluty", MessageBoxButton.OK, MessageBoxImage.Warning).ShowDialog();
                return;
            }
           
            if (nbpRate != null)
            {
                NbpRate = nbpRate.DefaultBuyRate;
                if (rate.UseNbpSpread)
                {
                    rate.DefaultBuyRate = NbpRate - rate.Spread;
                    rate.DefaultSellRate = NbpRate + rate.Spread;
                    rate.MaximumBuyRate = NbpRate;
                    rate.MinimalSellRate = NbpRate;
                }
            }

            CurrencyAmount = await _cashRegistryService.GetAmountForCurrency(currency);
            SelectedRate = new RateModel(rate);
        }

        #region Commands

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
                    new UserMessageBox("Wybierz walutę", MessageBoxButton.OK, MessageBoxImage.Warning).ShowMessage();
                    return;
                }
                if (Amount < 1)
                {
                    new UserMessageBox("Ilość musi być większa od zera", MessageBoxButton.OK, MessageBoxImage.Warning).ShowMessage();
                    return;
                }
                if (Amount > CurrencyAmount)
                {
                    new UserMessageBox("Wprowadzona ilość jest większa niż na stanie", MessageBoxButton.OK, MessageBoxImage.Warning).ShowMessage();
                    return;
                }
                if (SelectedRate != null)
                {
                    if (SelectedRate.UseNbpSpread)
                    {
                        if (SelectedType == TransactionType.Sell && ChangeRate < NbpRate)
                        {
                            new UserMessageBox("Kurs nie może być mniejszy od kursu NBP!", MessageBoxButton.OK, MessageBoxImage.Warning).ShowMessage();
                            return;
                        }
                        if (SelectedType == TransactionType.Buy && ChangeRate > NbpRate)
                        {
                            new UserMessageBox("Kurs nie może być większy od kursu NBP!", MessageBoxButton.OK, MessageBoxImage.Warning).ShowMessage();
                            return;
                        }
                    }
                    else
                    {
                        if (SelectedType == TransactionType.Sell && ChangeRate < SelectedRate.MinimalSellRate)
                        {
                            new UserMessageBox("Kurs nie może być mniejszy od minimalnego ustalonego kursu sprzedaży!", MessageBoxButton.OK, MessageBoxImage.Warning).ShowMessage();
                            return;
                        }
                        if (SelectedType == TransactionType.Buy && ChangeRate > SelectedRate.MaximumBuyRate)
                        {
                            new UserMessageBox("Kurs nie może być większy od maksymalnego ustalonego kursu kupna!", MessageBoxButton.OK, MessageBoxImage.Warning).ShowMessage();
                            return;
                        }
                    }
                }
                else
                {
                    if (ChangeRate < 0)
                    {
                        new UserMessageBox("Kurs nie może być mniejszy od zera!", MessageBoxButton.OK, MessageBoxImage.Warning).ShowMessage();
                        return;
                    }

                    new UserMessageBox("Nie posiadasz ustalonego kursu dla tej waluty, wymieniasz na własne ryzyko!", MessageBoxButton.OK, MessageBoxImage.Warning).ShowMessage();
                }

                Transaction ??= new TransactionModel();

                Transaction.Currency = SelectedCurrency;
                Transaction.Rate = FinalValue / Amount;
                Transaction.Quantity = Amount;
                Transaction.FinalValue = FinalValue;
                Transaction.TransactionType = SelectedType;

                var success = false;
                if (NewTransaction)
                {
                    success = await Parent.AddTransaction(Transaction);
                }
                else
                {
                    success = await Parent.EditTransaction(Transaction);
                }

                if (success)
                {
                    Parent.CancelForm();
                }
                else
                {
                    new UserMessageBox(string.Format("Nie udało się {0} transakcji", NewTransaction ? "dodać" : "edytować"), MessageBoxButton.OK, MessageBoxImage.Error).ShowMessage();
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
            Parent.CancelForm();
        }

        public ICommand SellCommand { get; private set; }
        private void SelectSell()
        {
            SelectedType = TransactionType.Sell; 
        }

        public ICommand BuyCommand { get; private set; }
        private void SelectBuy()
        {
            SelectedType = TransactionType.Buy;
        }

        #endregion
    }
}
