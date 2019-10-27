using Test.API.Models;

namespace Test.API.DAL.Repositories
{
    public class CallRepository : Repository<Call>
    {
        private new readonly TestContext context;

        public CallRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}
