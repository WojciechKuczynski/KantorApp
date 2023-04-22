using KantorClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.BLL.Services.Interfaces
{
    public interface ISettingsService
    {
        public Task<bool> LoadSettings();
        public List<Currency> Currencies { get; }
        public List<Rate> Rates { get; }
    }
}
