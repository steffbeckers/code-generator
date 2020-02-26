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
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
            // Before retrieval
            // #-#-#

            return await this.accountRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one account by Id.
		/// </summary>
		public async Task<Account> GetAccountByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
            // Before retrieval
            // #-#-#

            return await this.accountRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new account record.
		/// </summary>
        public async Task<Account> CreateAccountAsync(Account account)
        {
            // Validation
            if (account == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(account.Name))
                account.Name = account.Name.Trim();
            if (!string.IsNullOrEmpty(account.Website))
                account.Website = account.Website.Trim();
            if (!string.IsNullOrEmpty(account.Telephone))
                account.Telephone = account.Telephone.Trim();
            if (!string.IsNullOrEmpty(account.Email))
                account.Email = account.Email.Trim();
            if (!string.IsNullOrEmpty(account.Test))
                account.Test = account.Test.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
            // Before creation
            // #-#-#

			account = await this.accountRepository.InsertAsync(account);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
            // After creation
            // #-#-#

            return account;
        }

		/// <summary>
		/// Updates an existing account record by Id.
		/// </summary>
        public async Task<Account> UpdateAccountAsync(Account accountUpdate)
        {
            // Validation
            if (accountUpdate == null) { return null; }

            // Retrieve existing
            Account account = await this.accountRepository.GetByIdAsync(accountUpdate.Id);
            if (account == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(accountUpdate.Name))
                accountUpdate.Name = accountUpdate.Name.Trim();
            if (!string.IsNullOrEmpty(accountUpdate.Website))
                accountUpdate.Website = accountUpdate.Website.Trim();
            if (!string.IsNullOrEmpty(accountUpdate.Telephone))
                accountUpdate.Telephone = accountUpdate.Telephone.Trim();
            if (!string.IsNullOrEmpty(accountUpdate.Email))
                accountUpdate.Email = accountUpdate.Email.Trim();
            if (!string.IsNullOrEmpty(accountUpdate.Test))
                accountUpdate.Test = accountUpdate.Test.Trim();

            // Mapping
            account.Name = accountUpdate.Name;
            account.Website = accountUpdate.Website;
            account.Telephone = accountUpdate.Telephone;
            account.Email = accountUpdate.Email;
            account.Test = accountUpdate.Test;
            account.ParentAccountId = accountUpdate.ParentAccountId;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
            // Before update
            // #-#-#

			account = await this.accountRepository.UpdateAsync(account);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
            // After update
            // #-#-#

            return account;
        }

		/// <summary>
		/// Deletes an existing account record by Id.
		/// </summary>
        public async Task<Account> DeleteAccountByIdAsync(Guid accountId)
        {
            Account account = await this.accountRepository.GetByIdAsync(accountId);

            return await this.DeleteAccountAsync(account);
        }

		/// <summary>
		/// Deletes an existing account record.
		/// </summary>
        public async Task<Account> DeleteAccountAsync(Account account)
        {
            // Validation
            if (account == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
            // Before deletion
            // #-#-#

            await this.accountRepository.DeleteAsync(account);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
            // After deletion
            // #-#-#

            return account;
        }
    }
}
