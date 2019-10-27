using Test.API.Models;

namespace Test.API.DAL.Repositories
{
    public class EmailRepository : Repository<Email>
    {
        private new readonly TestContext context;

        public EmailRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}
