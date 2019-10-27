using Test.API.Models;

namespace Test.API.DAL.Repositories
{
	/// <summary>
	/// The repository for ProjectNotes in the data access layer.
	/// </summary>
    public class ProjectNoteRepository : Repository<ProjectNote>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the ProjectNote repository.
		/// </summary>
        public ProjectNoteRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}
