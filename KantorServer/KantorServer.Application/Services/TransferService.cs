using KantorServer.Application.Services.Interfaces;
using KantorServer.DAL;
using KantorServer.Model.Dtos;

namespace KantorServer.Application.Services
{
    public class TransferService : ITransferService
    {
        public DataContext DataContext { get; set; }

        public TransferService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<List<TransferDto>> GetAllTransfers()
        {
            var transfers = DataContext.Transfers.ToList();

            return TransferDto.Map(transfers);
        }
    }
}
