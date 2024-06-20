using System.Linq.Expressions;

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
        
        Task SaveAsync();

    }
}
