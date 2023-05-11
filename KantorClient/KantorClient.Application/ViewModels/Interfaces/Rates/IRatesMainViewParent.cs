using KantorClient.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Rates
{
    public interface IRatesMainViewParent
    {
        void AddRate(RateModel rateModel);
        void CancelAddEditWindow();
        void EditRate(RateModel rateModel);
    }
}
