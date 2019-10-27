using Test.API.Models;

namespace Test.API.DAL.Repositories
{
    public class ContactRepository : Repository<Contact>
    {
        private new readonly TestContext context;

        public ContactRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}
