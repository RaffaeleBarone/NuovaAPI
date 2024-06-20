using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Manager;

namespace NuovaAPI.Worker_Services
{
    public class VetrinaWorkerService : IVetrinaWorkerService
    {
        private readonly IVetrinaManager _vetrinaManager;

        public VetrinaWorkerService(IVetrinaManager vetrinaManager)
        {
            _vetrinaManager = vetrinaManager;
        }

        public Vetrina MapToVetrina(VetrinaDTO vetrinaDTO)
        {
            var vetrina = new Vetrina();
            vetrina.CodiceVetrina = (int)vetrinaDTO.CodiceVetrina;
            return vetrina;
        }

        public async Task AddVetrina(VetrinaDTO vetrinaDTO)
        {
            var vetrina = MapToVetrina(vetrinaDTO);
            await _vetrinaManager.AddVetrina(vetrina);
        }

        public async Task<IEnumerable<Vetrina>> GetVetrina()
        {
            return await _vetrinaManager.GetVetrine();
        }

        public async Task<Vetrina> GetVetrinaId(int id)
        {
            return await _vetrinaManager.GetIdVetrina(id);
        }

        public async Task<Vetrina> PutVetrina(int id, VetrinaDTO vetrinaDTO)
        {
            return await _vetrinaManager.ModificaVetrina(id, vetrinaDTO);
        }

        public async Task DeleteVetrina(int id)
        {
            await _vetrinaManager.RemoveVetrina(id);
        }
    }
}
