using KantorClient.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KantorClient.DAL.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private string _serviceAddress;
        private string _kantor;
        public string ServiceAddress => GetServiceAddress();
        public string Kantor => GetKantor();

        private string GetKantor()
        {
            return _kantor ?? LoadKantor();
        }

        private string LoadKantor()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            _kantor = config.GetSection("KantorSymbol").Value;
            return _kantor;
        }

        private string GetServiceAddress()
        {
            return _serviceAddress ?? LoadServiceAddress();
        }

        private string LoadServiceAddress()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            _serviceAddress = config.GetSection("ServerConnection").Value;
            return _serviceAddress;
        }
    }
}
