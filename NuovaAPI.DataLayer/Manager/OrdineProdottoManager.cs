using Microsoft.EntityFrameworkCore;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Infrastructure;

namespace NuovaAPI.DataLayer.Manager
{
    public class OrdineProdottoManager : IOrdineProdottoManager
    {
    
        private readonly IUnitOfWork _unitOfWork;
        public OrdineProdottoManager(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public async Task AddOrdineProdotto(OrdineProdotto ordineProdotto)
        {
            _unitOfWork.OrdineProdottoRepository.Add(ordineProdotto);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<OrdineProdotto>> GetOrdiniProdotti(int idOrdine)
        {
            var ordiniProdotti =  await _unitOfWork.OrdineProdottoRepository.Get(null)
        .Include(op => op.Prodotto)
        .ToListAsync();
            return ordiniProdotti;
        }

        public async Task<OrdineProdotto> GetOrdineProdotto(int idOrdine, int idProdotto)
        {
            var ordineProdotto = await _unitOfWork.OrdineProdottoRepository
        .Get(op => op.IdOrdine == idOrdine && op.IdProdotto == idProdotto)
        .SingleOrDefaultAsync();
            return ordineProdotto;
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

            _unitOfWork.Save();
            return ordineProdottoDaModificare;
        }

        public async Task RemoveOrdineProdotto(int idOrdine, int idProdotto)
        {
            var ordineProdottoDaRimuovere = await GetOrdineProdotto(idOrdine, idProdotto);

            if (ordineProdottoDaRimuovere != null)
            {
                _unitOfWork.OrdineProdottoRepository.Delete(ordineProdottoDaRimuovere);
                _unitOfWork.Save();
            }
        }
    }
}
