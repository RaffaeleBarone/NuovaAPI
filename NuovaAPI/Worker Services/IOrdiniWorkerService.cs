using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Worker_Services
{
    public interface IOrdiniWorkerService
    {
        Ordini MapToOrdini(OrdiniDTO ordiniDTO);
        Task AddOrdine(OrdiniDTO ordiniDTO);
        Task<ICollection<Ordini>> GetOrdini();
        Task<Ordini> GetOrdineId(int id);
        Task<Ordini> PutOrdine(int id, OrdiniDTO ordiniDTO);
        Task DeleteOrdine(int id);
    }
}
