using KantorClient.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.CashRegistry
{
    public interface ICashRegistryMainViewParent
    {
        void AddRegistry(CashRegistryModel model);
        void CancelAddEditWindow();
        void EditRegistry(CashRegistryModel model);
    }
}
