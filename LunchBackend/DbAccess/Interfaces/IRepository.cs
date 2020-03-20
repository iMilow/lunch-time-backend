using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace LunchBackend.DbAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity: class
    {
        // get
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
        Task<int> GetCountAsync();
        IQueryable<TEntity> GetAllAsQueryable();
        IQueryable<TEntity> GetAllIncludedAsQueryable(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> GetSingleFullDataAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        // add
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        
        // remove
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        // update
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        
        //find
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}