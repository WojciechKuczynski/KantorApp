using KantorClient.Application.ViewModels.Interfaces.CashRegistry;
using KantorClient.BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.CashRegistry
{
    public class CashRegistryAddEditViewModel : ICashRegistryAddEditViewModel, INotifyPropertyChanged
    {
        public ICashRegistryMainViewParent Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Task Load(bool loaded)
        {
            throw new NotImplementedException();
        }

        public void LoadForm(CashRegistryModel model = null)
        {
            throw new NotImplementedException();
        }
    }
}
