using KantorClient.Application.ViewModels.Interfaces.CashRegistry;
using Prism.Commands;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.CashRegistry
{
    public class CashRegistryPlnViewModel : ICashRegistryPlnViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool Loading { get; set; }
        public decimal Quantity { get; set; }

        public ICashRegistryMainViewParent Parent { get; set; }
        public CashRegistryPlnViewModel(ICashRegistryMainViewParent parent)
        {
            Parent = parent;
            EditCommand = new DelegateCommand(Edit);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public Task Load(bool loaded)
        {
            return Task.CompletedTask;
        }

        public void LoadForm()
        {

        }

        public ICommand EditCommand { get; private set; }

        private void Edit()
        {
            try
            {
                Loading = true;
                Parent.SetPln(Quantity).GetAwaiter();
            }
            finally
            {
                Loading = false;
            }
        }
        public ICommand CancelCommand { get; private set; }
        private void Cancel()
        {
            try
            {
                Loading = true;
                Parent.CancelAddEditWindow();
            }
            finally
            {
                Loading = false;
            }
        }
    }
}
