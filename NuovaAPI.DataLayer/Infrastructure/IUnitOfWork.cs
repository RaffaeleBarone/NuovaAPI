using Microsoft.EntityFrameworkCore.Storage;
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
        IRepository<Taxonomy> TaxonomyRepository { get; }
        IRepository<Termini> TerminiRepository { get; }
        void Save();
        IDbContextTransaction BeginTransaction();
        void Commit();
        void Dispose();
        //Task BulkInsertAsync(List<Cliente> clienti);
        //Task BulkUpdateAsync(List<Cliente> clienti);
        Task BulkMergeAsync(List<Cliente> nuoviClienti, List<Cliente> clientiDaAggiornare, List<Taxonomy> listTaxonomies, List<Termini> listTermini);
        Task SaveAsync();
    }
}
