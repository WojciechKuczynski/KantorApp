using KantorServer.Application.Requests.Transfers;
using KantorServer.Application.Responses;
using KantorServer.Application.Responses.Transfers;
using KantorServer.Application.Services.Interfaces;
using KantorServer.Model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KantorServer.API.Controllers
{
    [ApiController]
    [Route("/transfers")]
    public class TransferController : BaseController
    {
        private readonly ITransferService _transferService;
        public TransferController(ISessionService sessionService, ITransferService transferService) : base(sessionService)
        {
            _transferService = transferService;
        }

        [HttpPost("/all")]
        public async Task<GetAllTransfersResponse> GetAllTransfers(GetAllTransfersRequest request)
        {
            var checkRes = await CheckRequestArgs<GetAllTransfersResponse>(request);
            if (checkRes != null) { return checkRes; }

            var transfers = await _transferService.GetAllTransfers();
            return new GetAllTransfersResponse(true, "", "") { Transfers = transfers };
        }
    }
}
