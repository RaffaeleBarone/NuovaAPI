using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Manager
{
    public interface IOrdineProdottoManager
    {
        Task AddOrdineProdotto(OrdineProdotto ordineProdotto);
        Task<IEnumerable<OrdineProdotto>> GetOrdiniProdotti(int idOrdine);
        Task<OrdineProdotto> GetOrdineProdotto(int idOrdine, int idProdotto);
        Task<OrdineProdotto> ModificaOrdineProdotto(int idOrdine, int idProdotto, OrdineProdottoDTO ordineProdottoDTO);
        Task RemoveOrdineProdotto(int idOrdine, int idProdotto);
    }
}
