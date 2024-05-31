using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Worker_Services
{
    public class ProdottoWorkerService
    {
        public Prodotto MapToProdotto(ProdottoDTO prodottoDTO)
        {
            var prodotto = new Prodotto();
            prodotto.Id = prodottoDTO.Id;
            prodotto.NomeProdotto = prodottoDTO.NomeProdotto;
            prodotto.Prezzo = prodottoDTO.Prezzo;
            prodotto.IdVetrina = prodottoDTO.IdVetrina;
            return prodotto;
        }
    }
}
