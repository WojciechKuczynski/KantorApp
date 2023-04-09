using KantorServer.Application.Services.Interfaces;
using KantorServer.DAL;
using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Services
{
    public class SettingsService : ISettingsService
    {
        public DataContext DataContext { get; }

        public SettingsService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<bool> AddKantor(KantorDto kantor, CancellationToken token)
        {
            try
            {
                var kantorInDb = DataContext.Kantors.FirstOrDefault(x => x.Id == kantor.Id);
                if (kantorInDb == null)
                {
                    var kantorEntity = kantor.ConvertToEntity();
                    await DataContext.Kantors.AddAsync(kantorEntity,token);
                }
                else
                {
                    kantorInDb.IdentificationKey = kantor.IdentificationKey;
                    kantorInDb.Name = kantor.Name;
                }
                await DataContext.SaveChangesAsync(token);
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
