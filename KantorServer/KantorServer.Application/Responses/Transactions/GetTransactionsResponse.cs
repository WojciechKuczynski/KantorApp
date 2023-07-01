using KantorServer.Model.Dtos;

namespace KantorServer.Application.Responses.Transactions
{
    [Serializable]
    public class GetTransactionsResponse : BaseServerResponse
    {
        public GetTransactionsResponse(bool isCorrect, string? successMsg = null, string? failMsg = null) : base(isCorrect, successMsg, failMsg)
        {
        }

        public GetTransactionsResponse()
        {
            
        }
        public List<TransactionDto> Transactions { get; set; }
    }
}
