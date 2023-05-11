using KantorClient.Model;
using KantorServer.Application.Requests.Rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorClient.DAL.Repositories.Interfaces
{
    public interface ISynchronizationRepository
    {
        Task SynchronizeTransactions(string synchronizationToken);
        Task SynchronizeRate(string synchronizationToken);
    }
}
