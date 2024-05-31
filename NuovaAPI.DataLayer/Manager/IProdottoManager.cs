using NuovaAPI.DataLayer.Entities;
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
        public Task<ICollection<Prodotto>> GetProdotti();
        public Task<Prodotto> GetIdProdotto(int id);
        public Task<bool> ModificaProdotto(Prodotto prodotto);
    }
}
