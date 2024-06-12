using Microsoft.Extensions.Logging;
using NuovaAPI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using NuovaAPI.Commons.DTO;

namespace NuovaAPI.DataLayer.Manager
{
    public class VetrinaManager : IVetrinaManager
    {
        private readonly AppDbContext _appDbContext;
        public VetrinaManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddVetrina(Vetrina vetrina)
        {
            _appDbContext.Vetrine.Add(vetrina);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Vetrina>> GetVetrine()
        {
            return _appDbContext.Vetrine.Include(v => v.ProdottiInVetrina).ToList();
        }

        public async Task<Vetrina> GetIdVetrina(int id)
        {
            return _appDbContext.Vetrine.Where(x => x.Id == id).Include(v => v.ProdottiInVetrina).SingleOrDefault();
        }

        public async Task RemoveVetrina(int id)
        {
            //using (var dbContextTransaction = _appDbContext.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        var vetrinaDaRimuovere = _appDbContext.Vetrine.Include(v => v.Id).SingleOrDefault(v => v.Id == id);

            //        if (vetrinaDaRimuovere != null)
            //        {
            //            _appDbContext.Vetrine.RemoveRange((IEnumerable<Vetrina>)vetrinaDaRimuovere.ProdottiInVetrina);
            //            _appDbContext.Vetrine.Remove(vetrinaDaRimuovere);

            //            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            //            ILogger logger = factory.CreateLogger("Program");
            //            logger.LogInformation("Modifiche avvenute!");

            //            await _appDbContext.SaveChangesAsync();
            //            dbContextTransaction.Commit();
            //        }
            //    }

            //    catch (Exception)
            //    {
            //        dbContextTransaction.Rollback();
            //    }

            using (var dbContextTransaction = _appDbContext.Database.BeginTransaction())
            {
                try
                {
                    var vetrinaDaRimuovere = _appDbContext.Vetrine.SingleOrDefault(v => v.Id == id);

                    if (vetrinaDaRimuovere != null)
                    {
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




        public async Task<Vetrina> ModificaVetrina(int id, VetrinaDTO vetrinaDTO)
        {
            var vetrinaDaModificare = await _appDbContext.Vetrine.FindAsync(id);

            if (vetrinaDaModificare == null)
            {
                throw new Exception("Prodotto non trovato");
            }

            if (vetrinaDTO.CodiceVetrina != null)
            {
                vetrinaDaModificare.CodiceVetrina = (int)vetrinaDTO.CodiceVetrina;
            }
            
            await _appDbContext.SaveChangesAsync();
            return vetrinaDaModificare;
        }
    }
}


