using KantorClient.Application.ViewModels.Interfaces.Transactions;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.Model.Consts;
using Prism.Commands.Ex;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
        private bool _locker;

        private CurrencyModel _selectedCurrency;
        private RateModel _selectedRate;
        private TransactionType _selectedType;

        #endregion


        private readonly ISettingsService _settingsService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public TransactionsAddEditViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            
            SelectedType = TransactionType.Sell;

            AddCommand = new DelegateCommand(Add);
            CancelCommand = new DelegateCommand(Cancel);
            SellCommand = new DelegateCommand(SelectSell);
            BuyCommand = new DelegateCommand(SelectBuy);
        }

        #region Properties

        public string AcceptTile => NewTransaction ? "DODAJ" : "EDYTUJ";

        public bool Loading { get; set; }

        public bool NewTransaction { get; set; }

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
                        Amount = Math.Round(value / _changeRate,MidpointRounding.ToNegativeInfinity);
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

        public string TransactionText { get; set; }

        public CurrencyModel SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                _selectedCurrency = value;
                AssignRateForCurrency(value);
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
                ChangeRate = SelectedType == TransactionType.Sell ? value.DefaultSellRate : value.DefaultBuyRate;
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
                    ChangeRate = value == TransactionType.Sell ? SelectedRate.DefaultSellRate : SelectedRate.DefaultBuyRate;
                }
            }
        }

        #endregion

        public Task Load()
        {
            Currencies = new ObservableCollection<CurrencyModel>(_settingsService.Currencies.Select(x => new CurrencyModel(x)));
            return Task.CompletedTask;
        }

        public void LoadForm(TransactionModel model = null)
        {
            LoadModel(model);
        }

        private void LoadModel(TransactionModel model)
        {
            if (model != null)
            {
                NewTransaction = false;
                SelectedCurrency = Currencies.FirstOrDefault(x => x.Symbol == model.Currency.Symbol);
                Transaction = model;
                _locker = true;
                ChangeRate = model.Rate;
                FinalValue = model.FinalValue;
                Amount = model.Quantity;
                SelectedType = model.TransactionType;
                _locker = false;
            }
            else
            {
                NewTransaction = true;
            }
        }

        private void AssignRateForCurrency(CurrencyModel currency)
        {
            var rate = _settingsService.Rates.FirstOrDefault(x => x.Currency.Id == currency.Id);
            if ( rate == null) 
            {
                MessageBox.Show("Nie ma Ratów dla tej waluty");
                return; 
            }
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
                    MessageBox.Show("Wybierz walutę!");
                    return;
                }
                if (Amount < 1)
                {
                    MessageBox.Show("Ilość musi być większa od zera!");
                    return;
                }
                if (SelectedRate != null)
                {
                    if (SelectedType == TransactionType.Sell && ChangeRate < SelectedRate.MinimalSellRate)
                    {
                        MessageBox.Show("Kurs nie może być mniejszy od minimalnego ustalonego kursu sprzedaży!");
                        return;
                    }
                    if (SelectedType == TransactionType.Buy && ChangeRate > SelectedRate.MaximumBuyRate)
                    {
                        MessageBox.Show("Kurs nie może być większy od maksymalnego ustalonego kursu kupna!");
                        return;
                    }
                }
                else
                {
                    if (ChangeRate < 0)
                    {
                        MessageBox.Show("Kurs nie może być mniejszy od zera!");
                        return;
                    }

                    MessageBox.Show("Nie posiadasz ustalonego kursu dla tej waluty, wymieniasz na własne ryzyko!");
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
                    MessageBox.Show(string.Format("Nie udało się {0} transakcji", NewTransaction ? "dodać" : "edytować"));
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
            TransactionText = "TRANSAKCJA SPRZEDAŻY";
        }

        public ICommand BuyCommand { get; private set; }
        private void SelectBuy()
        {
            SelectedType = TransactionType.Buy;
            TransactionText = "TRANSAKCJA KUPNA";
        }

        #endregion
    }
}
