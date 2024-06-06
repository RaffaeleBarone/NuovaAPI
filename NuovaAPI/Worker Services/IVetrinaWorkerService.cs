using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Worker_Services
{
    public interface IVetrinaWorkerService
    {
        Vetrina MapToVetrina(VetrinaDTO vetrinaDTO);
        Task AddVetrina(VetrinaDTO vetrinaDTO);
        Task<ICollection<Vetrina>> GetVetrina();
        Task<Vetrina> GetVetrinaId(int id);
        Task<Vetrina> PutVetrina(int id, Vetrina vetrina);
        Task DeleteVetrina(int id);
    }
}
