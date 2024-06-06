using NuovaAPI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

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
            return await _appDbContext.Prodotti.ToListAsync();
        }

        public async Task<Prodotto> GetIdProdotto(int id)
        {
            return _appDbContext.Prodotti.Where(x => x.Id == id).SingleOrDefault();
        }

        public async Task<Prodotto> ModificaProdotto(int id, Prodotto prodotto)
        {
            var prodottoDaModificare = await _appDbContext.Prodotti.FindAsync(prodotto.Id);

            if(prodottoDaModificare == null)
            {
                throw new Exception("Prodotto non trovato");
            }

            prodottoDaModificare.NomeProdotto = prodotto.NomeProdotto;
            prodottoDaModificare.Prezzo = prodotto.Prezzo;
            prodottoDaModificare.IdVetrina = prodotto.IdVetrina;

            await _appDbContext.SaveChangesAsync();
            return prodottoDaModificare;
        }
    }
}
