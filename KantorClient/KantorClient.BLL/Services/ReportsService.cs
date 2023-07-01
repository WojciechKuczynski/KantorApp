using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.Repositories.Interfaces;
using KantorClient.DAL.RequestArgs;
using KantorClient.DAL.ResponseArgs;
using KantorServer.Application.Requests.Transactions;

namespace KantorClient.BLL.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _reportsRepository;
        private readonly IAuthenticationService _authenticationService;

        public ReportsService(IReportsRepository reportsRepository, IAuthenticationService authenticationService)
        {
            _reportsRepository = reportsRepository;
            _authenticationService = authenticationService;

        }

        public async Task<ReportsSettingsResponseArgs> GetReportsSettings()
        {
            if (_authenticationService.UserSession != null)
            {
                return await _reportsRepository.GetReportsSettings(_authenticationService.UserSession.SynchronizationKey);
            }
            return null;
        }

        public async Task<List<TransactionReportModel>> GetTransactions(TransactionsRequestArgs request)
        {
            if (_authenticationService.UserSession != null)
            {
                var req = new GetTransactionsRequest
                {
                    Currencies = request.Currencies ?? new List<string>(),
                    DateFrom = request.DateFrom,
                    DateTo = request.DateTo,
                    Kantors = request.Kantors ?? new List<long>(),
                    Users = request.Users ?? new List<long>(),
                };
                req.SynchronizationKey = _authenticationService.UserSession.SynchronizationKey;
                var res =  await _reportsRepository.GetTransactions(req);
                return res.Select(x => new TransactionReportModel(x)).ToList();
            }
            return new List<TransactionReportModel>();
        }
    }
}
