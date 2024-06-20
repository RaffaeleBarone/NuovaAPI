using Microsoft.EntityFrameworkCore.Storage;
using NuovaAPI.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _appDbContext;
        private readonly IRepository<Cliente> clienteRepository;
        private readonly IRepository<Prodotto> prodottoRepository;
        private readonly IRepository<Vetrina> vetrinaRepository;
        private readonly IRepository<Ordini> ordiniRepository;
        private readonly IRepository<OrdineProdotto> ordineProdottoRepository;
        private IDbContextTransaction _transaction;

        public IRepository<Cliente> ClienteRepository { get { return clienteRepository; } }
        public IRepository<Prodotto> ProdottoRepository { get { return prodottoRepository; } }
        public IRepository<Vetrina> VetrinaRepository { get { return vetrinaRepository; } }
        public IRepository<Ordini> OrdiniRepository { get { return ordiniRepository; } }
        public IRepository<OrdineProdotto> OrdineProdottoRepository { get { return  ordineProdottoRepository; } }    

        public UnitOfWork(AppDbContext appDbContext, IRepository<Cliente> clienteRepository, IRepository<Prodotto> prodottoRepository, IRepository<Vetrina> vetrinaRepository, IRepository<Ordini> ordiniRepository, IRepository<OrdineProdotto> ordineProdottoRepository)
        {
            _appDbContext = appDbContext;
            this.clienteRepository = clienteRepository;
            this.prodottoRepository = prodottoRepository;
            this.vetrinaRepository = vetrinaRepository;
            this.ordiniRepository = ordiniRepository;
            this.ordineProdottoRepository = ordineProdottoRepository;
            disposed = false;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = _appDbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }

            catch
            {
                _transaction.Rollback();
            }

            finally
            {
                _transaction.Dispose();
            }
        }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                _appDbContext.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
