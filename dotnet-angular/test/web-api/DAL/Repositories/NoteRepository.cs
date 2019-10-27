using System;
using System.Linq;
using Test.API.Models;

namespace Test.API.DAL.Repositories
{
	/// <summary>
	/// The repository for Notes in the data access layer.
	/// </summary>
    public class NoteRepository : Repository<Note>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Note repository.
		/// </summary>
        public NoteRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

    }
}
