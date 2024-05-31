using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Worker_Services
{
    public interface IProdottoWorkerService
    {
        Prodotto MapToProdotto(ProdottoDTO prodottoDTO);
        Task AddProduct(ProdottoDTO prodottoDTO);
    }
}
