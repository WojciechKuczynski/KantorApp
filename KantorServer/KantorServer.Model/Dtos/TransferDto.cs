using KantorServer.Model.Consts;

namespace KantorServer.Model.Dtos
{
    [Serializable]
    public class TransferDto
    {
        public long Id { get; set; }
        public TransferType TransferType { get; set; }
        public virtual KantorDto Kantor { get; set; }
        public virtual UserDto User { get; set; }
        public decimal TransferValue { get; set; }
        public CurrencyDto TransferCurrency { get; set; }
        public string Notes { get; set; }
        public long ExternalId { get; set; }
        public bool Valid { get; set; }
        public long? Parent { get; set; }
        public bool Edited { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime? DeletionDate { get; set; }

        public TransferDto()
        {

        }

        public TransferDto(Transfer transfer)
        {
            Id = transfer.Id;
            TransferType = transfer.TransferType;
            Kantor = new KantorDto(transfer.Kantor);
            User = new UserDto(transfer.User);
            TransferValue = transfer.TransferValue;
            TransferCurrency = new CurrencyDto(transfer.TransferCurrency);
            Notes = transfer.Notes;
            ExternalId = transfer.ExternalId;
            Valid = transfer.Valid;
            Parent = transfer.Parent;
            Edited = transfer.Edited;
            TransferDate = transfer.TransferDate;
            DeletionDate = transfer.DeletionDate;
        }

        public static List<TransferDto> Map(IEnumerable<Transfer> transfers) => transfers.Select(x => new TransferDto(x)).ToList();

        public Transfer ConvertToEntity()
        {
            var entity = new Transfer();
            if (Id > 0)
                entity.Id = Id;
            entity.TransferType = TransferType;
            entity.Kantor = Kantor.ConvertToEntity();
            entity.TransferValue = TransferValue;
            entity.TransferCurrency = TransferCurrency.ConvertToEntity();
            entity.Notes = Notes;
            entity.User = User.ConvertToEntity();
            entity.ExternalId = ExternalId;
            entity.Valid = Valid;
            entity.Parent = Parent;
            entity.Edited = Edited;
            entity.TransferDate = TransferDate;
            entity.DeletionDate = DeletionDate;
            return entity;
        }
    }
}
