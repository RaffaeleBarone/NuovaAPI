using EFCore.BulkExtensions;
using NuovaAPI.DataLayer.Entities;
using System.Linq.Expressions;
using Z.BulkOperations;

namespace NuovaAPI.DataLayer.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetById(int id);
        void Add(TEntity entity);
        void Update(TEntity entityToUpdate);
        TEntity Delete(int id);
        TEntity Delete(TEntity entityToDelete);
        //Task BulkInsertAsync(List<Cliente> clienti);
        //Task BulkUpdateAsync(List<Cliente> clienti);
        Task BulkMergeAsync(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> bulkConfig);
        Task BulkMergeAsync(IEnumerable<TEntity> entities);
        Task SaveAsync();
    }
}
