using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;

namespace KantorClient.BLL.Services
{
    public class TransfersService : ITransfersService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICashRegistryService _cashRegistryService;
        public TransfersService(ITransferRepository transferRepository, IAuthenticationService authenticationService, ICashRegistryService cashRegistryService)
        {
            _transferRepository = transferRepository;
            _authenticationService = authenticationService;
            _cashRegistryService = cashRegistryService;

        }

        public async Task<TransferModel> AddTransfer(TransferModel transfer, UserSession userSession)
        {
            try
            {
                var trans = transfer.Map();
                trans.User = userSession;
                var addedTrans = await _transferRepository.AddTransfer(trans);
                await HandleCashRegistry(addedTrans);
                return new TransferModel(addedTrans);
            }
            catch { }
            return null;
        }

        public async Task<bool> DeleteTransfer(TransferModel transfer)
        {
            try
            {
                var trans = transfer.Map();
                var deletedTrans = await _transferRepository.DeleteTransfer(trans);
                await HandleCashRegistry(deletedTrans);
                return deletedTrans != null;
            }
            catch { }
            return false;
        }

        public async Task<TransferModel> EditTransfer(TransferModel transfer, UserSession userSession)
        {
            try
            {
                var trans = transfer.Map();
                trans.User = userSession;
                var editedTrans = await _transferRepository.EditTransfer(trans);
                if (editedTrans == null)
                    return null;
                await HandleCashRegistry(editedTrans);
                return new TransferModel(editedTrans);
            }
            catch { }
            return null;
        }

        public async Task<List<TransferModel>> GetLocalTransfers()
        {
            try
            {
                if (_authenticationService.UserSession != null)
                {
                    var transfers = await _transferRepository.GetLocalTransfers(_authenticationService.UserSession);
                    if (transfers == null)
                    {
                        return new List<TransferModel>();
                    }

                    var transferModels = transfers.Select(x => new TransferModel(x));
                    return transferModels.ToList();
                }
            }
            catch (Exception ex)
            {
                //TODO: logging
            }
            return new List<TransferModel>();
        }

        private async Task HandleCashRegistry(Transfer trans)
        {
            if (trans == null)
                return;

            switch (trans.Type)
            {
                case Model.Consts.TransferType.TransferIn:
                    await _cashRegistryService.AddCurrency(trans.TransferCurrency, trans.TransferValue);
                    break;
                case Model.Consts.TransferType.TransferOut:
                    await _cashRegistryService.AddCurrency(trans.TransferCurrency, (-1) * trans.TransferValue);
                    break;
            }
        }
    }
}
