using Test.API.Models;

namespace Test.API.DAL.Repositories
{
    public class AccountRepository : Repository<Account>
    {
        private new readonly TestContext context;

        public AccountRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}
