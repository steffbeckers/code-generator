using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.API.Models;

namespace Test.API.DAL.Repositories
{
	/// <summary>
	/// The repository for AccountNotes in the data access layer.
	/// </summary>
    public class AccountNoteRepository : Repository<AccountNote>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the AccountNote repository.
		/// </summary>
        public AccountNoteRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public AccountNote GetByAccountAndNoteId(Guid accountId, Guid noteId)
        {
            return this.context.AccountNote
                .Where(x => x.AccountId == accountId && x.NoteId == noteId)
                .SingleOrDefault();
        }

		public AccountNote GetByNoteAndAccountId(Guid noteId, Guid accountId)
        {
            return this.context.AccountNote
                .Where(x => x.NoteId == noteId && x.AccountId == accountId)
                .SingleOrDefault();
        }
    }
}
