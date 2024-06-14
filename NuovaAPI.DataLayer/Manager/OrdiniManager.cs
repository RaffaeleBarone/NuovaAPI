using Microsoft.Extensions.Logging;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using System.Data.Entity;

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
            _appDbContext.Ordini.Add(acquisto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Ordini>> GetAcquisti()
        {
            //return _appDbContext.Ordini.Include(o => o.ProdottiAcquistati).ToList();
            return _appDbContext.Ordini.ToList();
        }

        public async Task<Ordini> GetIdAcquisto(int id)
        {
            //return _appDbContext.Ordini.Where(x => x.Id == id).Include(o => o.ProdottiAcquistati).SingleOrDefault();
            return await _appDbContext.Ordini.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Ordini> ModificaOrdine(int id, OrdiniDTO ordiniDTO)
        {
            var ordineDaModificare = await _appDbContext.Ordini.FindAsync(id);

            if (ordineDaModificare == null)
            {
                throw new Exception("Ordine non trovato");
            }

            if (ordiniDTO.CodiceOrdine != null)
            {
                ordineDaModificare.CodiceOrdine = ordiniDTO.CodiceOrdine;
            }

            await _appDbContext.SaveChangesAsync();
            return ordineDaModificare;
        }

        public async Task RemoveVetrina(int id)
        {
            using (var dbContextTransaction = _appDbContext.Database.BeginTransaction())
            {
                try
                {
                    var ordineDaRimuovere = _appDbContext.Ordini.SingleOrDefault(v => v.Id == id);

                    if (ordineDaRimuovere != null)
                    {
                        _appDbContext.Ordini.Remove(ordineDaRimuovere);

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
