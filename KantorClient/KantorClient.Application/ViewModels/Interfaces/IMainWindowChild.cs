using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces
{
    public interface IMainWindowChild
    {
        IMainWindowContainer Parent { get; set; }
        Task Load(bool loaded = false);
        Task OnShow();
    }
}
