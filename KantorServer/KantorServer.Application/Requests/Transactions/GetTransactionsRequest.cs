namespace KantorServer.Application.Requests.Transactions
{
    [Serializable]
    public class GetTransactionsRequest : BaseServerRequest
    {
        public GetTransactionsRequest()
        {
            Kantors = new List<long>();
            Users = new List<long>();
        }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public IEnumerable<long> Kantors { get; set; }
        public IEnumerable<long> Users { get; set; }
        public IEnumerable<string> Currencies { get; set; }
    }
}
