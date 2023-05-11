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
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Transactions
{
    public class TransactionsMainViewModel : ITransactionsMainViewModel, INotifyPropertyChanged
    {
        private readonly ITransactionsService _transactionsService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public IMainWindowContainer Parent { get; set; }
        public ITransactionsAddEditViewModel AddEditVM { get; set; }
        public bool AddEditVisible { get; set; }

        public TransactionsMainViewModel(ITransactionsService transactionsService, ITransactionsAddEditViewModel addEditVM)
        {
            _transactionsService = transactionsService;
            AddEditVM = addEditVM;
            AddEditVM.Parent = this;

            AddTransactionCommand = new DelegateCommand(AddTransaction);
        }

        public ObservableCollection<TransactionModel> Transactions { get; set; }

        public ICommand AddTransactionCommand { get; private set; }
        private void AddTransaction()
        {
            AddEditVM.LoadForm();
            AddEditVisible = true;
        }

        public async Task Load(bool loaded)
        {
            var transactions = await _transactionsService.GetLocalTransactions();
            Transactions = new ObservableCollection<TransactionModel>(transactions);
            if (loaded)
            {
                await AddEditVM.Load();
            }
        }

        public void CancelForm()
        {
            AddEditVisible = false;
        }
    }
}
