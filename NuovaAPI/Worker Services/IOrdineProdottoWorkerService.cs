using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Worker_Services
{
    public interface IOrdineProdottoWorkerService
    {
        OrdineProdotto MapToOrdineProdotto(OrdineProdottoDTO ordineProdottoDTO);
        Task AddOrdineProdotto(OrdineProdottoDTO ordineProdottoDTO);
        Task<IEnumerable<OrdineProdotto>> GetOrdiniProdotti(int idOrdine);
        Task<OrdineProdotto> GetOrdineProdottoById(int idOrdine, int idProdotto);
        Task<OrdineProdotto> PutOrdineProdotto(int idOrdine, int idProdotto, OrdineProdottoDTO ordineProdottoDTO);
        Task DeleteOrdineProdotto(int idOrdine, int idProdotto);
    }
}
