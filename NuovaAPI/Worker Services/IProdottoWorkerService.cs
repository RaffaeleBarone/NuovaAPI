using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Worker_Services
{
    public interface IProdottoWorkerService
    {
        Prodotto MapToProdotto(ProdottoDTO prodottoDTO);
        Task AddProduct(ProdottoDTO prodottoDTO);
        Task<IEnumerable<ProdottoDTO>> GetProduct(string nomeProdotto = null, string orderBy = null, bool ascending = true, int pageNumber = 1, int pageSize = 10);
        Task<Prodotto> GetProductId(int id);
        Task<Prodotto> PutProduct(int id, ProdottoDTO prodottoDTO);
        Task DeleteProduct(int id);
    }
}
