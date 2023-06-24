namespace KantorClient.DAL.Repositories.Interfaces
{
    public interface ISynchronizationRepository
    {
        Task SynchronizeTransactions(string synchronizationToken);
        Task SynchronizeRate(string synchronizationToken);
        Task SynchronizeTransfers(string synchronizationToken);
    }
}
