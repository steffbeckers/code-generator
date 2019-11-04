using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
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

		public async Task<Note> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Notes
                .SingleOrDefaultAsync(x => x.Id == id);
        }

    }
}
