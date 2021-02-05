using CodeGenOutput.Models;

namespace CodeGenOutput.API.DAL.Repositories
{
    public class AccountRepository : Repository<Account>
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Add additional repository methods for account here.
    }
}
