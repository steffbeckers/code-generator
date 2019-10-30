using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Test.API.Models;

namespace Test.API.DAL.Repositories
{
	/// <summary>
	/// The repository for Accounts in the data access layer.
	/// </summary>
    public class AccountRepository : Repository<Account>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Account repository.
		/// </summary>
        public AccountRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<Account> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Accounts
                .Include(x => x.Addresses)
                .Include(x => x.Contacts)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

    }
}
