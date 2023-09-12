using KantorClient.Model.Consts;

namespace KantorClient.Model
{
    public class Transfer : BaseModel
    {
        public TransferType Type { get; set; }
        public decimal TransferValue { get; set; }
        public virtual Currency TransferCurrency { get; set; }
        public virtual UserSession User { get; set; }
        public string Notes { get; set; }
        public long? Parent { get; set; }
        public long? ExternalId { get; set; }
        public bool Synchronized { get; set; }
        public bool Edited { get; set; }
        public bool Valid { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime? DeletionDate { get; set; }
    }
}
