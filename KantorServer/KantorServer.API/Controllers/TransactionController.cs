using KantorServer.Application.Requests.Transactions;
using KantorServer.Application.Responses.Transactions;
using KantorServer.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KantorServer.API.Controllers
{
    [ApiController]
    [Route("/transactions")]
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ISessionService sessionService, ITransactionService transactionService) : base(sessionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("synchronize")]
        public async Task<SynchronizeTransactionResponse> SynchronizeTransactions(SynchronizeTransactionRequest request)
        {
            var checkRes = await CheckRequestArgs<SynchronizeTransactionResponse>(request);
            if (checkRes != null) { return checkRes; }

            var res = await _transactionService.SynchronizeTransaction(request.Transaction, request.SynchronizationKey);
            return new SynchronizeTransactionResponse(res != null, "Synchronizacja przeszła pomyślnie", "Nie udało się zsynchronizować transakcji")
            {
                Transaction = res
            };
        }

        [HttpPost("get")]
        public async Task<GetTransactionsResponse> GetTransactions(GetTransactionsRequest request)
        {
            var checkRes = await CheckRequestArgs<GetTransactionsResponse>(request);
            if (checkRes != null) { return checkRes; }

            var res = await _transactionService.GetTransactions(request);

            return new GetTransactionsResponse(res != null, "", "Nie znaleziono transakcji")
            {
                Transactions = res
            };
        }
    }
}
