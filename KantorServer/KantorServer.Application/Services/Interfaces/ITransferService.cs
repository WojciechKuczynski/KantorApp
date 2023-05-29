using KantorServer.Model.Dtos;

namespace KantorServer.Application.Services.Interfaces
{
    public interface ITransferService : IService
    {
        public Task<List<TransferDto>> GetAllTransfers();
        Task<List<TransferDto>> AddTransfer(List<TransferDto> transfer);
        Task<TransferDto> SynchronizeTransfer(TransferDto transfer, string notificationKey);
    }
}
