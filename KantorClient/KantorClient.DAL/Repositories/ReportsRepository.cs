using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.DAL.ResponseArgs;
using KantorClient.DAL.ServerCommunication;
using KantorServer.Application.Requests.Reports;
using KantorServer.Application.Requests.Transactions;
using KantorServer.Application.Responses.Reports;
using KantorServer.Application.Responses.Transactions;
using KantorServer.Model.Dtos;

namespace KantorClient.DAL.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly IConfigurationRepository _configurationRepository;

        public ReportsRepository(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        public async Task<ReportsSettingsResponseArgs> GetReportsSettings(string synchronizationKey)
        {
            var request = new ReportsSettingsRequest()
            {
                SynchronizationKey = synchronizationKey
            };
            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/reports/settings", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<ReportsSettingsRequest, ReportsSettingsResponse>(requestContext, request);

            return new ReportsSettingsResponseArgs
            {
                Kantors = response.Kantors,
                Users = response.Users,
            };
        }

        public async Task<List<TransactionDto>> GetTransactions(GetTransactionsRequest request)
        {
            var requestContext = new RequestContext($"{_configurationRepository.ServiceAddress}/transactions/get", RestSharp.Method.Post);
            var response = await ServerConnectionHandler.ExecuteFunction<GetTransactionsRequest, GetTransactionsResponse>(requestContext, request);
            return response?.Transactions ?? new List<TransactionDto>(); 
        }
    }
}
