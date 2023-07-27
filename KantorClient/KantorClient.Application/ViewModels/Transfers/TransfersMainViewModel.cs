using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Transfers;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.Common.Extentions;
using KantorServer.Model.Consts;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Transfers
{
    public class TransfersMainViewModel : ITransfersMainViewModel, INotifyPropertyChanged
    {
        private readonly ITransfersService _transfersService;
        private readonly IAuthenticationService _authenticationService;
        public event PropertyChangedEventHandler? PropertyChanged;

        private List<TransferModel> TransfersCollection { get; set; }

        private bool _showDeleted;

        public IMainWindowContainer Parent { get; set; }
        public ITransfersAddEditViewModel AddEditVM { get; set; }
        public bool AddEnabled => CanAdd && !FormOpened;
        public bool EditEnabled => CanEdit && SelectedTransfer != null && !FormOpened;
        public bool FormOpened { get; set; }
        public bool Loading { get; set; }
        public bool ShowDeleted
        {
            get { return _showDeleted; }
            set
            {
                _showDeleted = value;
                RefreshTransferList();
            }
        }

        // Permissions
        private bool CanAdd { get; set; }
        private bool CanEdit { get; set; }
        public bool CanDelete { get; private set; }


        public ObservableCollection<TransferModel> Transfers { get; set; }
        public TransferModel SelectedTransfer { get; set; }

        public TransfersMainViewModel(ITransfersService transfersService, ISettingsService settingsService, IAuthenticationService authenticationService, ICashRegistryService cashRegistryService)
        {
            _transfersService = transfersService;
            _authenticationService = authenticationService;

            AddEditVM = new TransfersAddEditViewModel(settingsService, cashRegistryService);
            AddEditVM.Parent = this;

            AddTransferCommand = new DelegateCommand(AddTransfer);
            RefreshCommand = new DelegateCommand(Refresh);
            EditTransferCommand = new DelegateCommand(EditTransfer);
            RemoveTransferCommand = new DelegateCommand<TransferModel>(RemoveTransfer);

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
            FormOpened = false;
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
                CanAdd = _authenticationService.UserSession.HasUserPermission(PermissionKeys.Transfer.AddTransfer);
                CanEdit = _authenticationService.UserSession.HasUserPermission(PermissionKeys.Transfer.EditTransfer);
                CanDelete = _authenticationService.UserSession.HasUserPermission(PermissionKeys.Transfer.DeleteTransfer);
            }
        }

        public Task OnShow()
        {
            Refresh();
            return Task.CompletedTask;
        }

        private void RefreshTransferList()
        {
            Transfers = new ObservableCollection<TransferModel>(TransfersCollection.Where(x => (x.Valid || ShowDeleted) && !x.Edited));
        }

        #region Commands
        public ICommand AddTransferCommand { get; private set; }
        private void AddTransfer()
        {
            AddEditVM.LoadForm();
            FormOpened = true;
        }

        public ICommand EditTransferCommand { get; private set; }
        private void EditTransfer()
        {
            AddEditVM.LoadForm(SelectedTransfer);
            FormOpened = true;
        }

        public ICommand RefreshCommand { get; private set; }
        private async void Refresh()
        {
            try
            {
                Loading = true;
                TransfersCollection = await _transfersService.GetLocalTransfers();
                RefreshTransferList();
            }
            finally
            {
                Loading = false;
            }
        }

        public ICommand RemoveTransferCommand { get; private set; }
        private async void RemoveTransfer(TransferModel model)
        {
            try
            {
                if (!model.Valid)
                {
                    return;
                }

                var deleted = await _transfersService.DeleteTransfer(model);
                if (deleted)
                {
                    model.Valid = false;
                    model.DeletionDate = DateTime.Now;
                    Refresh();
                }
            }
            catch
            {

            }
        }
        #endregion
    }
}
