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
        // TODO: private readonly Account...Repository account...Repository;

		/// <summary>
		/// The constructor of the Account business logic layer.
		/// </summary>
        public AccountBLL(
			AccountRepository accountRepository//,
			// TODO: Account...Repository account...Repository
		)
        {
            this.accountRepository = accountRepository;
            // TODO: this.Account...Repository = Account...Repository;
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
            return await this.accountRepository.GetByIdAsync(id);
        }

		/// <summary>
		/// Creates a new account record.
		/// </summary>
        public async Task<Account> CreateAccountAsync(Account account)
        {
            return await this.accountRepository.InsertAsync(account);
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

            // Mapping
            account.Name = accountUpdate.Name;
            account.Website = accountUpdate.Website;
            account.Telephone = accountUpdate.Telephone;
            account.Email = accountUpdate.Email;

            return await this.accountRepository.UpdateAsync(account);
        }

		// TODO
        //public async Task<League> LinkPlayerToLeagueAsync(LeaguePlayer leaguePlayer)
        //{
        //    LeaguePlayer leaguePlayerLink = this.leaguePlayerRepository.GetByLeagueAndPlayerId(leaguePlayer.LeagueId, leaguePlayer.PlayerId);
		//
        //    if (leaguePlayerLink == null)
        //    {
        //        await this.leaguePlayerRepository.InsertAsync(leaguePlayer);
        //    }
        //    else
        //    {
        //        // Mapping
        //        leaguePlayerLink.Handicap = leaguePlayer.Handicap;
		//
        //        await this.leaguePlayerRepository.UpdateAsync(leaguePlayerLink);
        //    }
		//
        //    return this.leagueRepository.GetWithPlayersById(leaguePlayer.LeagueId);
        //}

		// TODO
        //public async Task<League> UnlinkPlayerFromLeagueAsync(LeaguePlayer leaguePlayer)
        //{
        //    LeaguePlayer leaguePlayerLink = this.leaguePlayerRepository.GetByLeagueAndPlayerId(leaguePlayer.LeagueId, leaguePlayer.PlayerId);
		//
        //    if (leaguePlayerLink != null)
        //    {
        //        await this.leaguePlayerRepository.DeleteAsync(leaguePlayerLink);
        //    }

        //    return this.leagueRepository.GetWithPlayersById(leaguePlayer.LeagueId);
        //}

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
