using Microsoft.Extensions.Logging;
using NuovaAPI.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Manager
{
    public class VetrinaManager : IVetrinaManager
    {
        private readonly AppDbContext _appDbContext;
        public VetrinaManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddVetrina(int id)
        {
            var vetrina = new Vetrina { Id = id };
            _appDbContext.Vetrine.Add(vetrina);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Vetrina>> GetVetrine()
        {
            return await _appDbContext.Vetrine.ToListAsync();
        }

        public async Task<Vetrina> GetIdVetrina(int id)
        {
            return _appDbContext.Vetrine.Where(x => x.Id == id).SingleOrDefault();
        }

        public async Task RemoveVetrina(int id)
        {
            using (var dbContextTransaction = _appDbContext.Database.BeginTransaction())
            {
                try
                {
                    var vetrinaDaRimuovere = _appDbContext.Vetrine.Include(v => v.Id).SingleOrDefault(v => v.Id == id);

                    if (vetrinaDaRimuovere != null)
                    {
                        _appDbContext.Vetrine.RemoveRange((IEnumerable<Vetrina>)vetrinaDaRimuovere.ProdottiInVetrina);
                        _appDbContext.Vetrine.Remove(vetrinaDaRimuovere);

                        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
                        ILogger logger = factory.CreateLogger("Program");
                        logger.LogInformation("Modifiche avvenute!");

                        await _appDbContext.SaveChangesAsync();
                        dbContextTransaction.Commit();
                    }
                }

                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }
    }
}


