using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Manager;

namespace NuovaAPI.Worker_Services
{
    public class ProdottoWorkerService : IProdottoWorkerService
    {
        private readonly IProdottoManager _prodottoManager;
        private readonly AppDbContext _appDbContext;

        public ProdottoWorkerService(IProdottoManager prodottoManager, AppDbContext appDbContext)
        {
            _prodottoManager = prodottoManager;
            _appDbContext = appDbContext;
        }
        public Prodotto MapToProdotto(ProdottoDTO prodottoDTO)
        {
            var prodotto = new Prodotto();
            //prodotto.Id = prodottoDTO.Id;
            prodotto.NomeProdotto = prodottoDTO.NomeProdotto;
            prodotto.Prezzo = (float)prodottoDTO.Prezzo;
            prodotto.IdVetrina = prodottoDTO.IdVetrina;
            prodotto.QuantitaDisponibile = (int)prodottoDTO.Quantita;
            //prodotto.IdOrdine = prodottoDTO.IdOrdine;

            return prodotto;
        }

        public async Task AddProduct(ProdottoDTO prodottoDTO)
        {
            var prodotto = MapToProdotto(prodottoDTO);


            await _prodottoManager.AddProdotto(prodotto);
        }

        public async Task<ICollection<Prodotto>> GetProduct()
        {
            try
            {
                return await _prodottoManager.GetProdotti();
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante il recupero dei prodotti: {ex.Message}");
            }
        }

        public async Task<Prodotto> GetProductId(int id)
        {
            try
            {
                return await _prodottoManager.GetIdProdotto(id);
            }
            catch(Exception ex)
            {
                throw new Exception ($"Errore durante il recupero del prodotto: {ex.Message}");
            }

        //    if (prodotto == null)
        //    {
        //        return Results.NotFound($"Prodotto con ID {id} non trovato.");
        //    }
        //    return Results.Ok(prodotto);
        //}
        //    catch (Exception ex)
        //    {
        //        return Results.Problem($"Errore durante il recupero del prodotto: {ex.Message}");
        //    }
}

        public async Task<Prodotto> PutProduct(int id, ProdottoDTO prodottoDTO)
        {
            return await _prodottoManager.ModificaProdotto(id, prodottoDTO);
        }

        public async Task DeleteProduct(int id)
        {
            await _prodottoManager.RemoveProdotto(id);
        }
    }
}
