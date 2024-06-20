using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Manager;

namespace NuovaAPI.Worker_Services
{
    public class OrdiniWorkerService : IOrdiniWorkerService
    {
        private readonly IOrdiniManager _ordiniManager;

        public OrdiniWorkerService(IOrdiniManager ordiniManager)
        {
            _ordiniManager = ordiniManager;
        }

        public Ordini MapToOrdini(OrdiniDTO ordiniDTO)
        {
            var ordine = new Ordini();
            ordine.CodiceOrdine = ordiniDTO.CodiceOrdine;
            return ordine;
        }

        public async Task AddOrdine(OrdiniDTO ordiniDTO)
        {
            var ordine = MapToOrdini(ordiniDTO);
            await _ordiniManager.AddAcquisto(ordine);
        }

        public async Task<IEnumerable<Ordini>> GetOrdini()
        {
            return await _ordiniManager.GetAcquisti();
        }

        public async Task<Ordini> GetOrdineId(int id)
        {
            return await _ordiniManager.GetIdAcquisto(id);
        }

        public async Task<Ordini> PutOrdine(int id, OrdiniDTO ordiniDTO)
        {
            return await _ordiniManager.ModificaOrdine(id, ordiniDTO);
        }

        public async Task DeleteOrdine(int id)
        {
            await _ordiniManager.RemoveVetrina(id);
        }
    }
}
