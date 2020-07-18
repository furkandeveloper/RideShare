using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RideShare.Web.Repositories.Abstractions
{
    public interface IBaseRepository<TEntity>
    {
        TEntity Add(TEntity value);
        Task<TEntity> AddAsync(TEntity value);
        void Delete(TEntity value);
        void Delete(string id);
        Task DeleteAsync(TEntity value);
        Task DeleteAsync(string id);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> Get();
        Task<List<TEntity>> GetMultiple(Expression<Func<TEntity, bool>> func);
        TEntity GetSingle(Expression<Func<TEntity, bool>> func);
        TEntity GetSingle(string id);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> query = null);
        Task<TEntity> GetSingleAsync(string id);
        void Replace(TEntity value);
        Task ReplaceAsync(TEntity value);
        void Update(TEntity value);
        Task UpdateAsync(TEntity value);
    }
}
