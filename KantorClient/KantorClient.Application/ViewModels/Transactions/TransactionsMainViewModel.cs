using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Transactions;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.Model;
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

namespace KantorClient.Application.ViewModels.Transactions
{
    public class TransactionsMainViewModel : ITransactionsMainViewModel, INotifyPropertyChanged
    {
        private readonly ITransactionsService _transactionsService;
        private readonly IAuthenticationService _authenticationService;

        public event PropertyChangedEventHandler? PropertyChanged;

        #region Variables

        private bool _showDeleted;

        #endregion

        #region Properties

        public IMainWindowContainer Parent { get; set; }
        
        public ITransactionsAddEditViewModel AddEditVM { get; set; }
        
        public bool AddEditVisible { get; set; }
        
        public ObservableCollection<TransactionModel> Transactions { get; set; }

        public List<TransactionModel> TransactionsCollection { get; set; }

        public bool ShowDeleted
        {
            get { return _showDeleted; }
            set
            {
                _showDeleted = value;
                RefreshTransactionList();
            }
        }

        #endregion

        public TransactionsMainViewModel(ITransactionsService transactionsService,IAuthenticationService authenticationService, ITransactionsAddEditViewModel addEditVM)
        {
            _authenticationService = authenticationService;
            _transactionsService = transactionsService;
            AddEditVM = addEditVM;
            AddEditVM.Parent = this;

            AddTransactionCommand = new DelegateCommand(AddTransaction);
            EditTransactionCommand = new DelegateCommand<TransactionModel>(Edit);
            DeleteTransactionCommand = new DelegateCommand<TransactionModel>(DeleteTransaction);
            RefreshCommand = new DelegateCommand(Refresh);
        }


        public async Task Load(bool loaded)
        {
            await RefreshTransactions();
            if (loaded)
            {
                await AddEditVM.Load();
            }
        }

        private async Task RefreshTransactions()
        {
            TransactionsCollection = await _transactionsService.GetLocalTransactions();
            RefreshTransactionList();
        }

        private void RefreshTransactionList()
        {
            Transactions = new ObservableCollection<TransactionModel>(TransactionsCollection.Where(x => (x.Valid || ShowDeleted) && !x.Edited));
        }

        public void CancelForm()
        {
            AddEditVisible = false;
        }

        public async Task<bool> AddTransaction(TransactionModel model)
        {
            var transaction = await _transactionsService.AddTransaction(model, _authenticationService.UserSession);
            Transactions.Add(transaction);
            return transaction != null;
        }

        public async Task<bool> EditTransaction(TransactionModel transactionModel)
        {
            var transaction = await _transactionsService.EditTransaction(transactionModel, _authenticationService.UserSession);
            await RefreshTransactions();
            return transaction != null;
        }


        #region Commands

        public ICommand RefreshCommand { get; private set; }
        private async void Refresh()
        {
            await RefreshTransactions();
        }

        public ICommand AddTransactionCommand { get; private set; }
        private void AddTransaction()
        {
            AddEditVM.LoadForm();
            AddEditVisible = true;
        }

        public ICommand EditTransactionCommand { get; private set; }
        private void Edit(TransactionModel model)
        {
            AddEditVM.LoadForm(model);
            AddEditVisible = true;
        }

        public ICommand DeleteTransactionCommand { get; private set; }
        private async void DeleteTransaction(TransactionModel model)
        {
            var transaction = await _transactionsService.DeleteTransaction(model);
            if (transaction)
            {
                model.Valid = false;
                model.DeletionDate = DateTime.Now;
                Refresh();
            }
        }


        #endregion
    }
}
