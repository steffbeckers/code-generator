using System;
using System.Threading.Tasks;

namespace CodeGen.API.DAL
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_dbContext);
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }
    }
}
