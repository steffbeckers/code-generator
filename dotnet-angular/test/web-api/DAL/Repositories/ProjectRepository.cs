using Test.API.Models;

namespace Test.API.DAL.Repositories
{
	/// <summary>
	/// The repository for Projects in the data access layer.
	/// </summary>
    public class ProjectRepository : Repository<Project>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Project repository.
		/// </summary>
        public ProjectRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}
