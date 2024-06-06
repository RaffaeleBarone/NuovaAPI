using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Manager;

namespace NuovaAPI.Worker_Services
{
    public class ProdottoWorkerService : IProdottoWorkerService
    {
        private readonly IProdottoManager _prodottoManager;

        public ProdottoWorkerService(IProdottoManager prodottoManager)
        {
            _prodottoManager = prodottoManager;
        }
        public Prodotto MapToProdotto(ProdottoDTO prodottoDTO)
        {
            var prodotto = new Prodotto();
            prodotto.Id = prodottoDTO.Id;
            prodotto.NomeProdotto = prodottoDTO.NomeProdotto;
            prodotto.Prezzo = prodottoDTO.Prezzo;
            prodotto.IdVetrina = prodottoDTO.IdVetrina;
            return prodotto;
        }

        public async Task AddProduct(ProdottoDTO prodottoDTO)
        {
            var prodotto = MapToProdotto(prodottoDTO);
            await _prodottoManager.AddProdotto(prodotto);
        }

        public async Task<ICollection<Prodotto>> GetProduct()
        {
            return await _prodottoManager.GetProdotti();
        }

        public async Task<Prodotto> GetProductId(int id)
        {
            return await _prodottoManager.GetIdProdotto(id);
        }

        public async Task<Prodotto> PutProduct(int id, Prodotto prodotto)
        {
            return await _prodottoManager.ModificaProdotto(id, prodotto);
        }

        public async Task DeleteProduct(int id)
        {
            await _prodottoManager.RemoveProdotto(id);
        }
    }
}
