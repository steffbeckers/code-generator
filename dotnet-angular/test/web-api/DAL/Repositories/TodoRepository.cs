using Test.API.Models;

namespace Test.API.DAL.Repositories
{
	/// <summary>
	/// The repository for Todoes in the data access layer.
	/// </summary>
    public class TodoRepository : Repository<Todo>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Todo repository.
		/// </summary>
        public TodoRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}
