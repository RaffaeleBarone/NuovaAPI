using Microsoft.EntityFrameworkCore.Storage;
using NuovaAPI.DataLayer.Entities;
using EFCore.BulkExtensions;

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
        private readonly IRepository<Taxonomy> taxonomyRepository;
        private readonly IRepository<Termini> terminiRepository;
        private IDbContextTransaction _transaction;

        public IRepository<Cliente> ClienteRepository { get { return clienteRepository; } }
        public IRepository<Prodotto> ProdottoRepository { get { return prodottoRepository; } }
        public IRepository<Vetrina> VetrinaRepository { get { return vetrinaRepository; } }
        public IRepository<Ordini> OrdiniRepository { get { return ordiniRepository; } }
        public IRepository<OrdineProdotto> OrdineProdottoRepository { get { return ordineProdottoRepository; } }
        public IRepository<Taxonomy> TaxonomyRepository { get { return taxonomyRepository; } }
        public IRepository<Termini> TerminiRepository { get { return terminiRepository; } } 

        public UnitOfWork(AppDbContext appDbContext, IRepository<Cliente> clienteRepository, IRepository<Prodotto> prodottoRepository, IRepository<Vetrina> vetrinaRepository, IRepository<Ordini> ordiniRepository, IRepository<OrdineProdotto> ordineProdottoRepository, IRepository<Taxonomy> taxonomyRepository, IRepository<Termini> terminiRepository)
        {
            _appDbContext = appDbContext;
            this.clienteRepository = clienteRepository;
            this.prodottoRepository = prodottoRepository;
            this.vetrinaRepository = vetrinaRepository;
            this.ordiniRepository = ordiniRepository;
            this.ordineProdottoRepository = ordineProdottoRepository;
            this.taxonomyRepository = taxonomyRepository;
            this.terminiRepository = terminiRepository;
            disposed = false;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public IDbContextTransaction BeginTransaction()
        {
            _transaction = _appDbContext.Database.BeginTransaction();
            return _transaction;
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
            if (disposing)
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

        //public async Task BulkInsertAsync(List<Cliente> clienti)
        //{
        //    await _appDbContext.BulkInsertAsync(clienti);
        //}

        //public async Task BulkUpdateAsync(List<Cliente> clienti)
        //{
        //    await _appDbContext.BulkUpdateAsync(clienti);
        //}

        public async Task BulkMergeAsync(List<Cliente> nuoviClienti, List<Cliente> clientiDaAggiornare, List<Taxonomy> listTaxonomies, List<Termini> listTermini)
        {
            await _appDbContext.BulkMergeAsync(nuoviClienti);
            await _appDbContext.BulkMergeAsync(clientiDaAggiornare);
            await _appDbContext.BulkMergeAsync(listTaxonomies);
            await _appDbContext.BulkMergeAsync(listTermini);
        }

        //public async Task BulkMergeAsync(IEnumerable<TEntity> entities)
        //{
        //    await _appDbContext.BulkMergeAsync(entities);
        //}

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
