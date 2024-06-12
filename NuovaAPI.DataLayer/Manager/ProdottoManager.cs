using NuovaAPI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using NuovaAPI.Commons.DTO;

namespace NuovaAPI.DataLayer.Manager
{
    public class ProdottoManager : IProdottoManager
    {
        private readonly AppDbContext _appDbContext;
        public ProdottoManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddProdotto(Prodotto prodotto)
        {
            //var prodotto = new Prodotto { NomeProdotto = nome, Prezzo = prezzo, IdVetrina = idVetrina };
            _appDbContext.Prodotti.Add(prodotto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveProdotto(int id)
        {
            var prodottoDaRimuovere = await _appDbContext.Prodotti.FirstOrDefaultAsync(x => x.Id == id);
            if (prodottoDaRimuovere != null)
            {
                _appDbContext.Prodotti.Remove(prodottoDaRimuovere);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Prodotto>> GetProdotti()
        {
            try
            {
                return await _appDbContext.Prodotti.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante il recupero dei prodotti dal database: {ex.Message}");
            }
        }

        public async Task<Prodotto> GetIdProdotto(int id)
        {
            try
            {
                return _appDbContext.Prodotti.Where(x => x.Id == id).SingleOrDefault();
            }

            catch(Exception ex)
            {
                throw new Exception($"Errore durante il recupero del prodotto dal database: {ex.Message}");
            }
        }

        public async Task<Prodotto> ModificaProdotto(int id, ProdottoDTO prodottoDTO)
        {
            var prodottoDaModificare = await _appDbContext.Prodotti.FindAsync(id);

            if (prodottoDaModificare == null)
            {
                throw new Exception("Prodotto non trovato");
            }

            if (prodottoDTO.NomeProdotto != null)
            {
                prodottoDaModificare.NomeProdotto = prodottoDTO.NomeProdotto;
            }
            if (prodottoDTO.Prezzo != null)
            {
                prodottoDaModificare.Prezzo = (float)prodottoDTO.Prezzo;
            }
            if (prodottoDTO.Quantita != null)
            {
                prodottoDaModificare.Quantita = (int)prodottoDTO.Quantita;
            }
            if (prodottoDTO.IdVetrina != null)
            {
                prodottoDaModificare.IdVetrina = prodottoDTO.IdVetrina;
            }
            if (prodottoDTO.IdOrdine != null)
            {
                prodottoDaModificare.IdOrdine = prodottoDTO.IdOrdine;
            }

            await _appDbContext.SaveChangesAsync();
            return prodottoDaModificare;
        }
    }
}
