using KantorClient.Model;
using KantorClient.Model.Consts;

namespace KantorClient.BLL.Models
{
    public class TransferModel
    {
        public long Id { get; set; }
        public TransferType Type { get; set; }
        public decimal TransferValue { get; set; }
        public CurrencyModel TransferCurrency { get; set; }

        public long UserId { get; set; }
        public bool Edited { get; set; }
        public bool Valid { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime? DeletionDate { get; set; }

        public TransferModel()
        {
            
        }

        public TransferModel(Transfer transfer)
        {
            Id = transfer.Id;
            Type = transfer.Type;
            TransferValue = transfer.TransferValue;
            TransferCurrency = new CurrencyModel(transfer.TransferCurrency);
            UserId = transfer.User.UserId;
            Edited = transfer.Edited;
            Valid = transfer.Valid;
            TransferDate = transfer.TransferDate;
            DeletionDate = transfer.DeletionDate;
        }

        public Transfer Map()
            => new()
            {
                Id = this.Id,
                Type = this.Type,
                TransferValue = this.TransferValue,
                TransferCurrency = this.TransferCurrency.Map(),
                Edited = this.Edited,
                Valid = this.Valid,
                TransferDate = this.TransferDate,
                DeletionDate = this.DeletionDate
            };
    }
}
