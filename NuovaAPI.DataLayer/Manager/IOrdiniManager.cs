using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Manager
{
    public interface IOrdiniManager
    {
        Task AddAcquisto(Ordini acquisto);
        Task<IQueryable<Ordini>> GetAcquisti();
        Task<Ordini> GetIdAcquisto(int id);
        Task<Ordini> ModificaOrdine(int id, OrdiniDTO ordiniDTO);
        Task RemoveVetrina(int id);
    }
}
