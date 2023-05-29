using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.Repositories;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;

namespace KantorClient.BLL.Services
{
    public class TransfersService : ITransfersService
    {
        private readonly ITransferRepository _transferRepository;

        public TransfersService(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public async Task<TransferModel> AddTransfer(TransferModel transfer, UserSession userSession)
        {
            try
            {
                var trans = transfer.Map();
                trans.User = userSession;
                var addedTrans = await _transferRepository.AddTransfer(trans);
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
                var addedTrans = await _transferRepository.DeleteTransfer(trans);
                return addedTrans != null;
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
                return new TransferModel(editedTrans);
            }
            catch { }
            return null;
        }

        public async Task<List<TransferModel>> GetLocalTransfers()
        {
            try
            {
                var transfers = await _transferRepository.GetLocalTransfers();
                if (transfers == null)
                {
                    return new List<TransferModel>();
                }

                var transferModels = transfers.Select(x => new TransferModel(x));
                return transferModels.ToList();
            }
            catch (Exception ex)
            {
                //TODO: logging
            }
            return new List<TransferModel>();
        }
    }
}
