using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<IEnumerable<Account>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Accounts
                .Include(x => x.ParentAccount)
                .ToListAsync();
        }

		public async Task<Account> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Accounts
                .Include(x => x.ParentAccount)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Account> GetByParentAccountId(Guid parentAccountId)
        {
            return this.context.Accounts
                .Where(t => t.ParentAccountId == parentAccountId)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Account>> GetByParentAccountIdAsync(Guid parentAccountId)
        //{
        //    return await this.context.Accounts
        //        .Where(t => t.ParentAccountId == parentAccountId)
        //        .ToListAsync();
        //}
    }
}