using KantorClient.BLL.Services.Interfaces;
using KantorClient.DAL.Repositories.Interfaces;
using System.ComponentModel;

namespace KantorClient.BLL.Services
{
    public class SynchronizationService : ISynchronizationService
    {
        private BackgroundWorker _rateSynchronization;
        private BackgroundWorker _transactionSynchronization;

        private bool _rateSynchro;
        private bool _transactionSynchro;

        private readonly ISynchronizationRepository _synchronizationRepository;
        private readonly IAuthenticationService _authenticationService;

        public SynchronizationService(ISynchronizationRepository synchronizationRepository, IAuthenticationService authenticationService)
        {
            _rateSynchronization = new BackgroundWorker();
            _transactionSynchronization = new BackgroundWorker();

            _rateSynchronization.DoWork += RateSynchronization_DoWork;
            _transactionSynchronization.DoWork += TransactionSynchronization_DoWork;

            _synchronizationRepository = synchronizationRepository;
            _authenticationService = authenticationService;

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
                    _synchronizationRepository.SynchronizeRate(synchroKey).GetAwaiter();
                    Thread.Sleep(arg);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void StartSynchronization()
        {
            _transactionSynchro = true;
            _transactionSynchronization.RunWorkerAsync(1 * 60 * 1000); // co 1 minut.

            _rateSynchro = true;
            _rateSynchronization.RunWorkerAsync(5 * 60 * 1000);
        }
    }
}
