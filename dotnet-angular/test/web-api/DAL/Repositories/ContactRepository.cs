using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.API.Models;

namespace Test.API.DAL.Repositories
{
	/// <summary>
	/// The repository for Contacts in the data access layer.
	/// </summary>
    public class ContactRepository : Repository<Contact>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Contact repository.
		/// </summary>
        public ContactRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Contact>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Contacts
                .Include(x => x.Account)
                .ToListAsync();
        }

		public async Task<Contact> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Contacts
                .Include(x => x.Account)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Contact> GetByAccountId(Guid accountId)
        {
            return this.context.Contacts
                .Where(t => t.AccountId == accountId)
                .ToList();
        }
    }
}
