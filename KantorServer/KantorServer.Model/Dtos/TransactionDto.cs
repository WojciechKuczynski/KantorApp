using KantorServer.Model.Consts;

namespace KantorServer.Model.Dtos
{
    [Serializable]
    public class TransactionDto
    {
        public long Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public CurrencyDto Currency { get; set; }
        public decimal Quantity { get; set; }
        public decimal FinalValue { get; set; }
        public decimal Rate { get; set; }
        public KantorDto Kantor { get; set; }
        public UserDto User { get; set; }
        public long? Parent { get; set; }
        public long ExternalId { get; set; }
        public bool Edited { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool Valid { get; set; }
        public DateTime? DeletionDate { get; set; }

        public TransactionDto()
        {

        }

        public TransactionDto(Transaction transaction)
        {
            Id = transaction.Id;
            TransactionType = transaction.TransactionType;
            Currency = new CurrencyDto(transaction.Currency);
            Quantity = transaction.Quantity;
            FinalValue = transaction.FinalValue;
            Rate = transaction.Rate;
            Kantor = new KantorDto(transaction.Kantor);
            User = new UserDto(transaction.User);
            ExternalId = transaction.ExternalId;
            Parent = transaction.Parent;
            Edited = transaction.Edited;
            Valid = transaction.Valid;
            TransactionDate = transaction.TransactionDate;
            DeletionDate = transaction.DeletionDate;
        }

        public static List<TransactionDto> Map(IEnumerable<Transaction> transactions) => transactions.Select(x => new TransactionDto(x)).ToList();

        public Transaction ConvertToEntity()
        {
            var transaction = new Transaction();
            if (Id > 0)
                transaction.Id = Id;
            transaction.TransactionType = TransactionType;
            transaction.Quantity = Quantity;
            transaction.FinalValue = FinalValue;
            transaction.Rate = Rate;
            transaction.ExternalId = ExternalId;
            transaction.Parent = Parent;
            transaction.TransactionDate = TransactionDate;
            transaction.Valid = Valid;
            transaction.DeletionDate = DeletionDate;
            transaction.Edited = Edited;

            if (Currency != null)
            {
                transaction.Currency = Currency.ConvertToEntity();
            }

            if (Kantor != null)
            {
                transaction.Kantor = Kantor.ConvertToEntity();
            }

            if (User != null)
            {
                transaction.User = User.ConvertToEntity();
            }

            return transaction;
        }
    }
}
