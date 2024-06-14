using Microsoft.EntityFrameworkCore;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.DataLayer.Manager
{
    public class OrdineProdottoManager : IOrdineProdottoManager
    {
        private readonly AppDbContext _appDbContext;
        public OrdineProdottoManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddOrdineProdotto(OrdineProdotto ordineProdotto)
        {
            _appDbContext.OrdiniProdotti.Add(ordineProdotto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<OrdineProdotto>> GetOrdiniProdotti(int idOrdine)
        {
            return await _appDbContext.OrdiniProdotti
        .Include(op => op.Prodotto)
        .ToListAsync();
        }

        public async Task<OrdineProdotto> GetOrdineProdotto(int idOrdine, int idProdotto)
        {
            return await _appDbContext.OrdiniProdotti
                .Where(op => op.IdOrdine == idOrdine && op.IdProdotto == idProdotto)
                .SingleOrDefaultAsync();
        }

        public async Task<OrdineProdotto> ModificaOrdineProdotto(int idOrdine, int idProdotto, OrdineProdottoDTO ordineProdottoDTO)
        {
            var ordineProdottoDaModificare = await GetOrdineProdotto(idOrdine, idProdotto);

            if (ordineProdottoDaModificare == null)
            {
                throw new Exception("OrdineProdotto non trovato");
            }

            if (ordineProdottoDTO.QuantitaAcquistata != null)
            {
                ordineProdottoDaModificare.QuantitaAcquistata = ordineProdottoDTO.QuantitaAcquistata;
            }

            await _appDbContext.SaveChangesAsync();
            return ordineProdottoDaModificare;
        }

        public async Task RemoveOrdineProdotto(int idOrdine, int idProdotto)
        {
            var ordineProdottoDaRimuovere = await GetOrdineProdotto(idOrdine, idProdotto);

            if (ordineProdottoDaRimuovere != null)
            {
                _appDbContext.OrdiniProdotti.Remove(ordineProdottoDaRimuovere);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
