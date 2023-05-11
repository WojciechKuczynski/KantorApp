using KantorClient.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.Rates
{
    public interface IRatesAddEditViewModel
    {
        IRatesMainViewParent Parent { get; set; }
        Task Load(bool loaded);

        void LoadForm(RateModel model = null);
    }
}
