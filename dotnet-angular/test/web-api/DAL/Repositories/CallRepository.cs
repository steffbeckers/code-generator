using Test.API.Models;

namespace Test.API.DAL.Repositories
{
	/// <summary>
	/// The repository for Calls in the data access layer.
	/// </summary>
    public class CallRepository : Repository<Call>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Call repository.
		/// </summary>
        public CallRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}
