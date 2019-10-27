using Test.API.Models;

namespace Test.API.DAL.Repositories
{
    /// <summary>
    /// The repository for Emails in the data access layer.
    /// </summary>
    public class EmailRepository : Repository<Email>
    {
        private new readonly TestContext context;

        /// <summary>
        /// The constructor of the Email repository.
        /// </summary>
        public EmailRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

    }
}
