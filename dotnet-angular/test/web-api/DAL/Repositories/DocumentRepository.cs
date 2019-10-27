using Test.API.Models;

namespace Test.API.DAL.Repositories
{
    public class DocumentRepository : Repository<Document>
    {
        private new readonly TestContext context;

        public DocumentRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}
