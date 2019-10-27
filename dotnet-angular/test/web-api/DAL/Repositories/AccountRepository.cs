using System;
using System.Linq;
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

    }
}
