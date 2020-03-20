using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LunchBackend.DbAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace LunchBackend.DbAccess
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity: class
    {
        protected readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);
        }
        
        public async Task AddRangeAsync (IEnumerable<TEntity> entities)
        {
            await context.Set<TEntity>().AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().UpdateRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetFullDataAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var result = context.Set<TEntity>().AsQueryable();

            if (include != null)
                result = include(result);

            return await result.Where(predicate).ToListAsync();
        }
        
        public IQueryable<TEntity> GetAllIncludedAsQueryable(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) {
            var result = context.Set<TEntity>().AsQueryable();

            if (include != null)
                result = include(result);

            return result;
        }

        public async Task<TEntity> GetSingleFullDataAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var result = context.Set<TEntity>().AsQueryable();

            if (include != null)
                result = include(result);

            return await result.Where(predicate).FirstOrDefaultAsync();
        }

        public TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public async Task<int> GetCountAsync()
        {
            return await context.Set<TEntity>().CountAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public IQueryable<TEntity> GetAllAsQueryable() {
            return context.Set<TEntity>().AsQueryable();
        }

        public void Remove(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }

        public IQueryable<TEntity> Include(string propertyName)
        {
            return context.Set<TEntity>().Include(propertyName);
        }

        public IQueryable<TEntity> Include(Expression<Func<TEntity, object>> predicate)
        {
            return context.Set<TEntity>().Include(predicate);
        }
    }
}