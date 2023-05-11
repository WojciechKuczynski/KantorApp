using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Rates;
using KantorClient.Application.Views.Rates;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using Prism.Commands.Ex;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Rates
{
    public class RatesMainViewModel : IRatesMainViewModel, INotifyPropertyChanged
    {
        private readonly ISettingsService _settingsService;

        public event PropertyChangedEventHandler? PropertyChanged;
        public IMainWindowContainer Parent { get; set; }
        public IRatesAddEditViewModel AddEditVM { get; set; }

        public bool AddEditVisible { get; set; }

        public ObservableCollection<RateModel> Rates { get; set; }

        public RateModel SelectedRate { get; set; }

        public RatesMainViewModel(ISettingsService settingsService, IRatesAddEditViewModel ratesAddEditVM)
        {
            _settingsService = settingsService;
            AddEditVM = ratesAddEditVM;
            AddRateCommand = new DelegateCommand(AddRate);
        }
        public async Task Load(bool loaded)
        {
            if (loaded) 
            {
                Rates = new ObservableCollection<RateModel>(_settingsService.Rates.Select(x => new RateModel(x)));
            }
            await AddEditVM.Load(loaded);
            AddEditVM.Parent = this;
        }

        public ICommand AddRateCommand { get; private set; }
        private void AddRate()
        {
            AddEditVM.LoadForm(SelectedRate);
            AddEditVisible = true;
        }

        public void CancelAddEditWindow()
        {
            AddEditVisible = false;
        }

        public async void AddRate(RateModel rateModel)
        {
            if (rateModel != null)
            {
                var rate = await _settingsService.AddRate(RateModel.Map(rateModel));
                Rates.Add(new RateModel(rate));
            }
        }

        public async void EditRate(RateModel rateModel)
        {
            if (rateModel != null)
            {
                var rate = await _settingsService.AddRate(RateModel.Map(rateModel));
            }
        }
    }
}
