using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NuovaAPI.DataLayer.Entities;
using System.Linq.Expressions;
using Z.BulkOperations;

namespace NuovaAPI.DataLayer.Infrastructure.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null)
            {
                return _dbSet.Where(predicate);
            }
            return _dbSet;
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }


        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _appDbContext.Entry(entityToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public virtual TEntity Delete(int id)
        {
            TEntity entityToDelete = _dbSet.Find(id);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
            }
            return entityToDelete;
        }

        public virtual TEntity Delete(TEntity entityToDelete)
        {
            if (_appDbContext.Entry(entityToDelete).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
            return entityToDelete;
        }

        //public async Task BulkInsertAsync(List<Cliente> clienti)
        //{
        //    await _appDbContext.BulkInsertAsync(clienti);
        //}

        //public async Task BulkUpdateAsync(List<Cliente> clienti)
        //{
        //    await _appDbContext.BulkUpdateAsync(clienti);
        //}

        public async Task BulkMergeAsync(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> bulkConfig)
        {
            await _appDbContext.BulkMergeAsync(entities.ToList(), bulkConfig);
        }

        public async Task BulkMergeAsync(IEnumerable<TEntity> entities)
        {
            await _appDbContext.BulkMergeAsync(entities.ToList());
        }


        public virtual async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }      
    }
}
