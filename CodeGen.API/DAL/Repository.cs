using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeGen.API.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> GetDbSet();
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string include = "",
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
        );
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(TEntity entity);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<TEntity> GetDbSet()
        {
            return _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string include = "",
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
        )
        {
            IQueryable<TEntity> query = GetDbSet();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (string property in include.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await GetDbSet().FindAsync(id);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Update(entity);
            return Task.FromResult(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            TEntity entity = await GetByIdAsync(id);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbContext.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
