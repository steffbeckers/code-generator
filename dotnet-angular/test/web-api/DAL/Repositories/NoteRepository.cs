using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<IEnumerable<Note>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Notes
                .Include(x => x.AccountNote)
                    .ThenInclude(x => x.Account)
                .ToListAsync();
        }

		public async Task<Note> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Notes
                .Include(x => x.AccountNote)
                    .ThenInclude(x => x.Account)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Account> GetAccountsOfNoteById(Guid id)
        {
            List<AccountNote> accountNote = this.context.AccountNote
                .Include(x => x.Account)
                .Where(x => x.NoteId == id)
                .ToList();

            return accountNote.Select(x => x.Account).ToList();
        }
    }
}
