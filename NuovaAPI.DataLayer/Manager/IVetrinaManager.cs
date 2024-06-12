using NuovaAPI.DataLayer.Entities;
using NuovaAPI.Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Manager
{
    public interface IVetrinaManager
    {
        Task AddVetrina(Vetrina vetrina);
        Task<ICollection<Vetrina>> GetVetrine();
        Task<Vetrina> GetIdVetrina(int id);
        Task RemoveVetrina(int id);
        Task<Vetrina> ModificaVetrina(int id, VetrinaDTO vetrinaDTO);
    }
}
