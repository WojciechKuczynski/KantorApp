using KantorClient.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KantorClient.DAL.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private string _serviceAddress;
        public string ServiceAddress => GetServiceAddress();

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
