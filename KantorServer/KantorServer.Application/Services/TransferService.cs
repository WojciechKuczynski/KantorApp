using KantorServer.Application.Services.Interfaces;
using KantorServer.DAL;
using KantorServer.Model;
using KantorServer.Model.Dtos;
using Microsoft.EntityFrameworkCore;

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
            var transfers = DataContext.Transfers.Where(x => !x.Edited).ToList();

            return TransferDto.Map(transfers);
        }

        public async Task<List<TransferDto>> AddTransfer(List<TransferDto> transfers)
        {
            try
            {
                var transfersForDb = transfers.Select(x => x.ConvertToEntity());
                await DataContext.Transfers.AddRangeAsync(transfersForDb);
                await DataContext.SaveChangesAsync();

                var addedTransfers = TransferDto.Map(transfersForDb);
                return addedTransfers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TransferDto> SynchronizeTransfer(TransferDto transfer, string notificationKey)
        {
            try
            {
                var transferInDb = await DataContext.Transfers.FirstOrDefaultAsync(x => x.ExternalId == transfer.ExternalId);
                if (transferInDb != null)
                {
                    // EDIT
                    return await EditTransfer(transfer, transferInDb, notificationKey);
                }

                // ADD
                var transferForDb = transfer.ConvertToEntity();
                var userSession = await DataContext.UserSessions
                                    .Include(x => x.Kantor)
                                    .Include(x => x.User)
                                    .FirstOrDefaultAsync(x => x.SynchronizationKey == notificationKey);
                if (userSession == null)
                {
                    return null;
                }

                transferForDb.User = userSession.User;
                transferForDb.Kantor = userSession.Kantor;
                transferForDb.TransferCurrency = await DataContext.Currencies.FirstOrDefaultAsync(x => x.Symbol == transfer.TransferCurrency.Symbol);
                if (transfer.Parent != null)
                {
                    var parent = await DataContext.Transactions
                                    .Include(x => x.Kantor)
                                    .FirstOrDefaultAsync(x => x.ExternalId == transfer.Parent.Value && x.Kantor.Id == userSession.Kantor.Id);
                    if (parent != null)
                    {
                        transfer.Parent = parent.Id;
                    }
                }

                await DataContext.Transfers.AddRangeAsync(transferForDb);
                await DataContext.SaveChangesAsync();

                var addedTransaction = new TransferDto(transferForDb);
                return addedTransaction;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<TransferDto> EditTransfer(TransferDto transfer, Transfer transferInDb, string notificationKey)
        {
            try
            {
                var userSession = await DataContext.UserSessions
                                .Include(x => x.Kantor)
                                .Include(x => x.User)
                                .FirstOrDefaultAsync(x => x.SynchronizationKey == notificationKey);

                if (userSession == null)
                {
                    return null;
                }

                transferInDb.User = userSession.User;
                transferInDb.Kantor = userSession.Kantor;
                transferInDb.TransferCurrency = await DataContext.Currencies.FirstOrDefaultAsync(x => x.Symbol == transfer.TransferCurrency.Symbol);
                transferInDb.TransferType = transfer.TransferType;
                transferInDb.TransferValue = transfer.TransferValue;
                transferInDb.TransferDate = transfer.TransferDate;
                transferInDb.DeletionDate = transfer.DeletionDate;
                transferInDb.Parent = transfer.Parent;
                transferInDb.Valid = transfer.Valid;
                transferInDb.Edited = transfer.Edited;

                if (transfer.Parent != null)
                {
                    // where transferId from Kantor equals Parent value
                    var parent = await DataContext.Transfers
                                    .Include(x => x.Kantor)
                                    .FirstOrDefaultAsync(x => x.ExternalId == transfer.Parent.Value && x.Kantor.Id == userSession.Kantor.Id);
                    if (parent != null)
                    {
                        transfer.Parent = parent.Id;
                    }
                }

                await DataContext.SaveChangesAsync();
                return new TransferDto(transferInDb);
            }
            catch (Exception ex) { }
            return null;
        }
    }
}
