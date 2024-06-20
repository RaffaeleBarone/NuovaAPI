using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Infrastructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Cliente> ClienteRepository { get; }
        IRepository<Prodotto> ProdottoRepository { get; }
        IRepository<Vetrina> VetrinaRepository { get; }
        IRepository<Ordini> OrdiniRepository { get; }
        IRepository<OrdineProdotto> OrdineProdottoRepository { get; }
        void Save();
        void BeginTransaction();
        void Commit();
        void Dispose();


    }
}
