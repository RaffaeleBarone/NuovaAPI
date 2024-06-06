using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Worker_Services
{
    public interface IProdottoWorkerService
    {
        Prodotto MapToProdotto(ProdottoDTO prodottoDTO);
        Task AddProduct(ProdottoDTO prodottoDTO);
        Task<ICollection<Prodotto>> GetProduct();
        Task<Prodotto> GetProductId(int id);
        Task<Prodotto> PutProduct(int id, Prodotto prodotto);
        Task DeleteProduct(int id);
    }
}
