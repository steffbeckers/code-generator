using System;
using System.Threading.Tasks;

namespace CodeGenOutput.API.DAL
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task SaveChangesAsync();
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

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }
    }
}
