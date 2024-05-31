using NuovaAPI.Commons.DTO;
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
    }
}
