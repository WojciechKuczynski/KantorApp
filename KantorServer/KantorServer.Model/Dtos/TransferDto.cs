using KantorServer.Model.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Model.Dtos
{
    [Serializable]
    public class TransferDto
    {
        public long Id { get; set; }
        public TransferType TransferType { get; set; }
        public virtual KantorDto Kantor { get; set; }
        public decimal TransferValue { get; set; }
        public CurrencyDto TransferCurrency { get; set; }

        public TransferDto()
        {
            
        }

        public TransferDto(Transfer transfer)
        {
            Id = transfer.Id;
            TransferType = transfer.TransferType;
            Kantor = new KantorDto(transfer.Kantor);
            TransferValue = transfer.TransferValue;
            TransferCurrency = new CurrencyDto(transfer.TransferCurrency);
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
            return entity;
        }
    }
}
