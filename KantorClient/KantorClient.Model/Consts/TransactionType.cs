using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KantorClient.Model.Consts
{
    public enum TransactionType
    {
        [Display(Name = "SPRZEDAŻ")]
        Sell = 1,
        [Display(Name = "KUPNO")]
        Buy = 2
    }
}
