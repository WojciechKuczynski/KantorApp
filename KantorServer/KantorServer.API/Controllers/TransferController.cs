﻿using KantorServer.Application.Requests.Transfers;
using KantorServer.Application.Responses.Transfers;
using KantorServer.Application.Services.Interfaces;
using KantorServer.Model.Consts;
using Microsoft.AspNetCore.Mvc;

namespace KantorServer.API.Controllers
{
    [ApiController]
    [Route("/transfers")]
    public class TransferController : BaseController
    {
        private readonly ITransferService _transferService;
        public TransferController(ISessionService sessionService, ITransferService transferService, IUserPermissionService userPermissionService) : base(sessionService, userPermissionService)
        {
            _transferService = transferService;
        }

        [HttpPost("/all")]
        public async Task<GetAllTransfersResponse> GetAllTransfers(GetAllTransfersRequest request)
        {
            var checkRes = await CheckRequestArgs<GetAllTransfersResponse>(request, new[] { PermissionKeys.Transfer.ListTransfer });
            if (checkRes != null) { return checkRes; }

            var transfers = await _transferService.GetAllTransfers();
            return new GetAllTransfersResponse(true, "", "") { Transfers = transfers };
        }

        [HttpPost("synchronize")]
        public async Task<SynchronizeTransferResponse> SynchronizeTransactions(SynchronizeTransferRequest request)
        {
            var checkRes = await CheckRequestArgs<SynchronizeTransferResponse>(request);
            if (checkRes != null) { return checkRes; }

            var res = await _transferService.SynchronizeTransfer(request.Transfer, request.SynchronizationKey);
            return new SynchronizeTransferResponse(res != null, "Synchronizacja przeszła pomyślnie", "Nie udało się zsynchronizować transferu")
            {
                Transfer = res
            };
        }
    }
}
