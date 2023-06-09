using KantorClient.Application.ViewModels.Interfaces;
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
    public class CashRegistryMainViewModel : ICashRegistryMainViewModel, INotifyPropertyChanged
    {
        public IMainWindowContainer Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddRegistry(CashRegistryModel model)
        {
            throw new NotImplementedException();
        }

        public void CancelAddEditWindow()
        {
            throw new NotImplementedException();
        }

        public void EditRegistry(CashRegistryModel model)
        {
            throw new NotImplementedException();
        }

        public Task Load(bool loaded = false)
        {
            throw new NotImplementedException();
        }
    }
}
