using KantorClient.BLL.Services.Interfaces;
using KantorClient.Common.Exceptions;
using KantorClient.DAL.Repositories.Interfaces;
using System.ComponentModel;

namespace KantorClient.BLL.Services
{
    public class SynchronizationService : ISynchronizationService
    {
        private BackgroundWorker _rateSynchronization;
        private BackgroundWorker _transactionSynchronization;
        private BackgroundWorker _transferSynchronization;

        private bool _rateSynchro;
        private bool _transactionSynchro;
        private bool _transferSynchro;

        private readonly ISynchronizationRepository _synchronizationRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        public SynchronizationService(ISynchronizationRepository synchronizationRepository, IAuthenticationService authenticationService, ISettingsService settingsService)
        {
            _rateSynchronization = new BackgroundWorker();
            _transactionSynchronization = new BackgroundWorker();
            _transferSynchronization = new BackgroundWorker();

            _rateSynchronization.DoWork += RateSynchronization_DoWork;
            _transactionSynchronization.DoWork += TransactionSynchronization_DoWork;
            _transferSynchronization.DoWork += TransferSynchronization_DoWork;

            _synchronizationRepository = synchronizationRepository;
            _authenticationService = authenticationService;
            _settingsService = settingsService;

            _authenticationService.OnlineModeChanged += AuthenticationService_OnlineModeChanged;
        }

        private void AuthenticationService_OnlineModeChanged(object sender, bool newValue)
        {
            if (newValue)
            {
                StartSynchronization();
            }
            else
            {
                StopSynchronization();
            }
        }

        private void TransferSynchronization_DoWork(object? sender, DoWorkEventArgs e)
        {
            int arg = (int)e.Argument;
            var synchroKey = _authenticationService.UserSession.SynchronizationKey;
            while (_transferSynchro)
            {
                try
                {
                    _synchronizationRepository.SynchronizeTransfers(synchroKey).GetAwaiter();
                    _settingsService.LoadRates().GetAwaiter();
                    Thread.Sleep(arg);
                }
                catch (ServerNotReachedException ex)
                {
                    _authenticationService.SetOnlineMode(false);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void TransactionSynchronization_DoWork(object? sender, DoWorkEventArgs e)
        {
            int arg = (int)e.Argument;
            // wysyłanie transakcji
            var synchroKey = _authenticationService.UserSession.SynchronizationKey;
            while (_transactionSynchro)
            {
                try
                {
                    _synchronizationRepository.SynchronizeTransactions(synchroKey).GetAwaiter();
                    Thread.Sleep(arg);
                }
                catch (ServerNotReachedException ex)
                {
                    _authenticationService.SetOnlineMode(false);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void RateSynchronization_DoWork(object? sender, DoWorkEventArgs e)
        {
            int arg = (int)e.Argument;
            var synchroKey = _authenticationService.UserSession.SynchronizationKey;
            // pobieranie i wysyłanie Ratów
            while (_rateSynchro)
            {
                try
                {
                    // wysyłanie ratów
                    _synchronizationRepository.SynchronizeRate(synchroKey).GetAwaiter();
                    // pobieranie ratów
                    _settingsService.LoadRates();
                    Thread.Sleep(arg);
                }
                catch (ServerNotReachedException ex)
                {
                    _authenticationService.SetOnlineMode(false);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void StartSynchronization()
        {
            while (_transactionSynchronization.IsBusy || _transferSynchronization.IsBusy || _rateSynchronization.IsBusy)
            {
                Thread.Sleep(1000 * 5);
            }

            if (!_transferSynchro)
            {
                _transactionSynchro = true;
                _transactionSynchronization.RunWorkerAsync(1 * 60 * 1000); // co 1 minut.
            }

            if (!_transferSynchro)
            {
                _transferSynchro = true;
                _transferSynchronization.RunWorkerAsync(1 * 60 * 1000); // co 1 minut.
            }

            if (!_rateSynchro)
            {
                _rateSynchro = true;
                _rateSynchronization.RunWorkerAsync(5 * 60 * 1000);
            }
        }

        private void StopSynchronization()
        {
            if (_transactionSynchro)
            {
                _transactionSynchro = false;
            }

            if (_transferSynchro)
            {
                _transferSynchro = false;
            }

            if (_rateSynchro)
            {
                _rateSynchro = false;
            }
        }
    }
}
