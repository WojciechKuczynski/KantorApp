using KantorClient.Application.ViewModels.Interfaces.Rates;
using KantorClient.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.CashRegistry
{
    public interface ICashRegistryAddEditViewModel
    {
        ICashRegistryMainViewParent Parent { get; set; }
        Task Load(bool loaded);

        void LoadForm(CashRegistryModel model = null);
    }
}
