using KantorClient.Application.ViewModels.Interfaces;
using KantorClient.Application.ViewModels.Interfaces.Rates;
using KantorClient.BLL.Models;
using KantorClient.BLL.Services.Interfaces;
using KantorClient.Model;
using Prism.Commands.Ex;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KantorClient.Application.ViewModels.Rates
{
    public class RatesAddEditViewModel : IRatesAddEditViewModel, INotifyPropertyChanged
    {
        private readonly ISettingsService _settingsService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public RateModel RateModel { get; set; }
        public List<CurrencyModel> Currencies { get; set; }

        public RatesAddEditViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            CancelCommand = new DelegateCommand(Cancel);
            AcceptCommand = new DelegateCommand(Accept);
        }

        public Task Load(bool loaded = false)
        {
            if (loaded)
            {
                Currencies = _settingsService.Currencies.Select(x => new CurrencyModel(x)).ToList();
            }
            return Task.CompletedTask;
        }
        public IRatesMainViewParent Parent { get; set; }

        public ICommand CancelCommand { get; private set; }

        private void Cancel()
        {
            Parent.CancelAddEditWindow();
        }

        public ICommand AcceptCommand { get; private set; }

        private void Accept()
        {
            if (RateModel.Id > 0)
            {
                Parent.EditRate(RateModel);
            }
            else
            {
                Parent.AddRate(RateModel);
            }
        }

        public void LoadForm(RateModel model = null)
        {
            if (model != null)
            {
                RateModel = model;
                RateModel.Currency = Currencies.FirstOrDefault(x => x.Id == model.Currency.Id);
            }
            else
            {
                RateModel = new RateModel() { Valid = true };
            }
        }
    }
}
