﻿using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Transfers;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace KantorClient.Application.ViewModels.Transfers
{
    public class TransfersMainViewModel : ITransfersMainViewModel, INotifyPropertyChanged
    {
        private readonly ITransfersService _transfersService;
        private readonly IAuthenticationService _authenticationService;
        public event PropertyChangedEventHandler? PropertyChanged;

        private List<TransferModel> TransfersCollection { get; set; }
        
        public IMainWindowContainer Parent { get; set; }
        public ITransfersAddEditViewModel AddEditVM { get; set; }
        public bool AddEditVisible { get; set; }
        public ObservableCollection<TransferModel> Transfers { get; set; }
        public TransferModel SelectedTransfer { get; set; }

        public TransfersMainViewModel(ITransfersService transfersService, ISettingsService settingsService, IAuthenticationService authenticationService)
        {
            _transfersService = transfersService;
            _authenticationService = authenticationService;

            AddEditVM = new TransfersAddEditViewModel(settingsService);
            AddEditVM.Parent = this;

            AddTransferCommand = new DelegateCommand(AddTransfer);

        }
        public async Task<bool> AddTransfer(TransferModel model)
        {
            var added = await _transfersService.AddTransfer(model, _authenticationService.UserSession);
            if (added != null) 
            {
                Transfers.Add(added);
                return true;
            }

            return false;
        }

        public void CancelForm()
        {
            AddEditVisible = false;
        }

        public async Task<bool> EditTransfer(TransferModel model)
        {
            var edited = await _transfersService.EditTransfer(model, _authenticationService.UserSession);
            
            if (edited != null)
            {
                var transfer = Transfers.FirstOrDefault(x => x.Id == edited.Id);
                if (transfer != null)
                {
                    transfer = edited;
                }

                return true;
            }

            return false;
        }

        public async Task Load(bool loaded = false)
        {
            if (loaded)
            {
                TransfersCollection = await _transfersService.GetLocalTransfers();
                Transfers = new ObservableCollection<TransferModel>(TransfersCollection);
                await AddEditVM.Load();
            }
        }

        public ICommand AddTransferCommand { get; private set; }
        private void AddTransfer()
        {
            AddEditVM.LoadForm(SelectedTransfer);
            AddEditVisible = true;
        }
    }
}