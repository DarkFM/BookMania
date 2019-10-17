using BookMania.Core.Entities;
using BookMania.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Infrastructure.Data
{
    /// <summary>
    /// A Generic Repository for all entity types
    /// </summary>
    /// <typeparam name="T">The entity model type</typeparam>
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly CatalogContext _dbContext;

        public EfRepository(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T[]> AddAsync(params T[] entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();

            return entities;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllOrderedAsync<TKey>(System.Linq.Expressions.Expression<Func<T, TKey>> expression)
        {
            return await _dbContext.Set<T>().OrderBy(expression).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task<int> CountAsync(ISpecification<T> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            throw new NotImplementedException();
        }
    }
}
