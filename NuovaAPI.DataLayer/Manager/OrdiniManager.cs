using NuovaAPI.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Manager
{
    public class OrdiniManager : IOrdiniManager
    {
        private readonly AppDbContext _appDbContext;
        public OrdiniManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAcquisto(Ordini acquisto)
        {
            _appDbContext.Acquisti.Add(acquisto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Ordini>> GetAcquisti()
        {
            return await _appDbContext.Acquisti.ToListAsync();
        }

        public async Task<Ordini> GetIdAcquisto(int id)
        {
            return _appDbContext.Acquisti.Where(x => x.Id == id).SingleOrDefault();
        }


    }
}
