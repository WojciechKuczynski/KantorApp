using KantorClient.Application.ViewModels.Interfaces.Transactions;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Transactions
{
    public class TransactionsAddEditViewModel : ITransactionsAddEditViewModel
    {
        private decimal _finalValue;
        private decimal _amount;
        private decimal _changeRate;

        private CurrencyModel _selectedCurrency;

        private readonly ISettingsService _settingsService;

        public TransactionsAddEditViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            AddCommand = new DelegateCommand(Add);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public decimal Amount
        {
            get { return _amount; }
            set 
            { 
                _amount = value; 
            }
        }

        public decimal FinalValue
        {
            get { return _finalValue; }
            set
            {
                _finalValue = value;
                //
                _changeRate = Math.Round(value / _amount,2);
            }
        }

        public decimal ChangeRate
        {
            get { return _changeRate; }
            set 
            { 
                _changeRate = value;
                // 
                _finalValue = value * _amount;
            }
        }

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

        public RateModel SelectedRate { get; set; }


        public Task Load()
        {
            return Task.CompletedTask;
        }

        public void LoadForm(TransactionModel model = null)
        {
            Currencies = new ObservableCollection<CurrencyModel>(_settingsService.Currencies.Select(x => new CurrencyModel(x)));
        }

        private void AssignRateForCurrency(CurrencyModel currency)
        {
            var rate = _settingsService.Rates.FirstOrDefault(x => x.Currency.Id == currency.Id);
            SelectedRate = new RateModel(rate);
        }

        public ICommand AddCommand { get; private set; }
        private void Add()
        {

        }

        public ICommand CancelCommand { get; private set; }
        private void Cancel()
        {
            Parent.CancelForm();
        }
    }
}
