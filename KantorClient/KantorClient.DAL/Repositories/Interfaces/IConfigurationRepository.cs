namespace KantorClient.DAL.Repositories.Interfaces
{
    public interface IConfigurationRepository
    {
        string ServiceAddress { get; }
        string Kantor { get; }
    }
}
