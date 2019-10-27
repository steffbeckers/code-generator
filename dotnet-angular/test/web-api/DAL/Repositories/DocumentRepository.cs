using Test.API.Models;

namespace Test.API.DAL.Repositories
{
    /// <summary>
    /// The repository for Documents in the data access layer.
    /// </summary>
    public class DocumentRepository : Repository<Document>
    {
        private new readonly TestContext context;

        /// <summary>
        /// The constructor of the Document repository.
        /// </summary>
        public DocumentRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

    }
}
