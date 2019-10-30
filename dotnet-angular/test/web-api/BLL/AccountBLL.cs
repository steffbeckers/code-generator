using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Accounts.
	/// </summary>
    public class AccountBLL
    {
        private readonly AccountRepository accountRepository;

		/// <summary>
		/// The constructor of the Account business logic layer.
		/// </summary>
        public AccountBLL(
			AccountRepository accountRepository
		)
        {
            this.accountRepository = accountRepository;
        }

		/// <summary>
		/// Retrieves all accounts.
		/// </summary>
		public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await this.accountRepository.GetAsync();
        }

		/// <summary>
		/// Retrieves one account by Id.
		/// </summary>
		public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            return await this.accountRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new account record.
		/// </summary>
        public async Task<Account> CreateAccountAsync(Account account)
        {
			// #-#-# {6B392F7F-C4B3-4E64-8703-AE95C834E86A}
			// Before creation

			// #-#-#

			account = await this.accountRepository.InsertAsync(account);

			// #-#-# {086618AE-01D1-4162-8C4C-03080741C2CB}
			// After creation

			// #-#-#

            return account;
        }

		/// <summary>
		/// Updates an existing account record by Id.
		/// </summary>
        public async Task<Account> UpdateAccountAsync(Guid id, Account accountUpdate)
        {
            // Retrieve existing
            Account account = await this.accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                return null;
            }

			// #-#-# {573CD65B-4771-4335-85AC-74C5FB2E2AC8}
			// Before update mapping
			// #-#-#

            // Mapping
            account.Name = accountUpdate.Name;
            account.Website = accountUpdate.Website;
            account.Telephone = accountUpdate.Telephone;
            account.Email = accountUpdate.Email;

            // #-#-# {61904A6D-4EB9-47DF-B58E-8DDA26B0FB8C}
            // Before update
            account.Name = account.Name.Trim();
			// #-#-#

			account = await this.accountRepository.UpdateAsync(account);

			// #-#-# {0DB85255-BF4B-462A-A3E9-847F47A6C1F0}
			// After update
            // TODO: Send email
			// #-#-#

            return account;
        }

		/// <summary>
		/// Deletes an existing account record by Id.
		/// </summary>
        public async Task<Account> DeleteAccountAsync(Account account)
        {
            await this.accountRepository.DeleteAsync(account);

            return account;
        }
    }
}
