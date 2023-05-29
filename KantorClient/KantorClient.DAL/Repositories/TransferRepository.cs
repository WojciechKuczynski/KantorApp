using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.Model;
using Microsoft.EntityFrameworkCore;

namespace KantorClient.DAL.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        public async Task<Transfer> AddTransfer(Transfer transfer)
        {
            using var dataContext = new DataContext();
            transfer.TransferCurrency = await dataContext.Currencies.FindAsync(transfer.TransferCurrency.Id);
            transfer.User = await dataContext.UserSessions.FindAsync(transfer.User.Id);
            if (transfer.User == null || transfer.TransferCurrency == null)
            {
                return null;
            }

            transfer.TransferDate = DateTime.Now;
            transfer.Valid = true;
            transfer.Synchronized = false;
            await dataContext.Transfers.AddAsync(transfer);
            await dataContext.SaveChangesAsync();
            return transfer;
        }

        public async Task<Transfer> DeleteTransfer(Transfer transfer)
        {
            using var dataContext = new DataContext();
            var transferInDb = await dataContext.Transfers.FindAsync(transfer.Id);
            if (transferInDb == null)
            {
                return null;
            }

            transferInDb.Valid = false;
            transferInDb.DeletionDate = DateTime.Now;
            transferInDb.Synchronized = false;
            await dataContext.SaveChangesAsync();
            return transferInDb;
        }

        public async Task<Transfer> EditTransfer(Transfer transfer)
        {
            using var dataContext = new DataContext();
            var transferInDb = await dataContext.Transfers.FindAsync(transfer.Id);
            if (transferInDb == null)
            {
                return null;
            }
            // delete old
            transferInDb.Valid = false;
            transferInDb.DeletionDate = DateTime.Now;
            transferInDb.Synchronized = false;
            transferInDb.Edited = true;

            //new
            var newTransfer = new Transfer()
            {
                Type = transfer.Type,
                TransferValue = transfer.TransferValue,
                Parent = transferInDb.Id,
                Valid = true,
                Synchronized = false,
                TransferDate = DateTime.UtcNow,
            };
            newTransfer.TransferCurrency = await dataContext.Currencies.FindAsync(transfer.TransferCurrency.Id);
            newTransfer.User = await dataContext.UserSessions.FindAsync(transfer.User.Id);

            await dataContext.AddAsync(newTransfer);
            await dataContext.SaveChangesAsync();
            return newTransfer;
        }

        public async Task<IEnumerable<Transfer>> GetLocalTransfers()
        {
            using var dataContext = new DataContext();
            var localTransfers = await dataContext.Transfers.Include(x => x.TransferCurrency).Include(x => x.User).Take(100).ToListAsync();

            return localTransfers;
        }
    }
}
