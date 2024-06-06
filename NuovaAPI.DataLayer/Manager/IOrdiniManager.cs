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
        Task<ICollection<Ordini>> GetAcquisti();
        Task<Ordini> GetIdAcquisto(int id);
    }
}
