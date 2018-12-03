using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xcelerator.Data;
using Xcelerator.Entity;
using Xcelerator.Repositories.Interfaces;

namespace Xcelerator.Repositories
{
    public class Repository<TEntity, T> : IRepository<TEntity, T> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _entities = _context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRangeAsync(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _entities.CountAsync();
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Any(predicate);
        }

        public virtual IEnumerable<TEntity> FindAll()
        {
            return _entities;
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate == null ? _entities : _entities.Where(predicate);
        }

        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }

        public virtual async Task<TEntity> GetAsync(T id)
        {
            return await _entities.FindAsync(id);
        }
    }
}
