namespace KantorClient.DAL.RequestArgs
{
    public class TransactionsRequestArgs
    {
        public DateTime? DateFrom {  get; set; }
        public DateTime? DateTo { get; set; }
        public IEnumerable<long> Kantors { get; set; }
        public IEnumerable<long> Users { get; set; }
        public IEnumerable<string> Currencies { get; set; }
    }
}
