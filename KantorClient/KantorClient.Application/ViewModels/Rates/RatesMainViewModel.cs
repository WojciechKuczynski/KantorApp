using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Rates;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace KantorClient.Application.ViewModels.Rates
{
    public class RatesMainViewModel : IRatesMainViewModel, INotifyPropertyChanged
    {
        private readonly ISettingsService _settingsService;

        public event PropertyChangedEventHandler? PropertyChanged;
        public IMainWindowContainer Parent { get; set; }

        public ObservableCollection<RateModel> Rates { get; set; }

        public void Load()
        {
            Rates = new ObservableCollection<RateModel>(_settingsService.Rates.Select(x => new RateModel(x)));
        }
    }
}
