using NuovaAPI.DataLayer.Entities;
using NuovaAPI.Commons.DTO; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Manager
{
    public interface IProdottoManager
    {
        public Task AddProdotto(Prodotto prodotto);
        public Task RemoveProdotto(int id);
        public Task<IEnumerable<ProdottoDTO>> GetProdotti(string nomeProdotto = null);
        public Task<Prodotto> GetIdProdotto(int id);
        public Task<Prodotto> ModificaProdotto(int id, ProdottoDTO prodottoDTO);
    }
}
