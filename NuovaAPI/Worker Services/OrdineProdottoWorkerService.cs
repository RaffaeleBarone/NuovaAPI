using AutoMapper;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Manager;

namespace NuovaAPI.Worker_Services
{
    public class OrdineProdottoWorkerService : IOrdineProdottoWorkerService
    {
        private readonly IOrdineProdottoManager _ordineProdottoManager;
        private readonly IMapper _mapper;


        public OrdineProdottoWorkerService(IOrdineProdottoManager ordineProdottoManager, IMapper mapper)
        {
            _ordineProdottoManager = ordineProdottoManager;
            _mapper = mapper;
        }

        public OrdineProdotto MapToOrdineProdotto(OrdineProdottoDTO ordineProdottoDTO)
        {
            return _mapper.Map<OrdineProdotto>(ordineProdottoDTO);
        }

        public async Task AddOrdineProdotto(OrdineProdottoDTO ordineProdottoDTO)
        {
            var ordineProdotto = MapToOrdineProdotto(ordineProdottoDTO);
            await _ordineProdottoManager.AddOrdineProdotto(ordineProdotto);
        }

        public async Task<ICollection<OrdineProdotto>> GetOrdiniProdotti(int idOrdine)
        {
            return await _ordineProdottoManager.GetOrdiniProdotti(idOrdine);
        }

        public async Task<OrdineProdotto> GetOrdineProdottoById(int idOrdine, int idProdotto)
        {
            return await _ordineProdottoManager.GetOrdineProdotto(idOrdine, idProdotto);
        }

        public async Task<OrdineProdotto> PutOrdineProdotto(int idOrdine, int idProdotto, OrdineProdottoDTO ordineProdottoDTO)
        {
            return await _ordineProdottoManager.ModificaOrdineProdotto(idOrdine, idProdotto, ordineProdottoDTO);
        }

        public async Task DeleteOrdineProdotto(int idOrdine, int idProdotto)
        {
            await _ordineProdottoManager.RemoveOrdineProdotto(idOrdine, idProdotto);
        }
    }
}

