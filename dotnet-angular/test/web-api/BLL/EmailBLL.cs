using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Emails.
	/// </summary>
    public class EmailBLL
    {
        private readonly EmailRepository emailRepository;
        // TODO: private readonly Email...Repository email...Repository;

		/// <summary>
		/// The constructor of the Email business logic layer.
		/// </summary>
        public EmailBLL(
			EmailRepository emailRepository//,
			// TODO: Email...Repository email...Repository
		)
        {
            this.emailRepository = emailRepository;
            // TODO: this.Email...Repository = Email...Repository;
        }

		/// <summary>
		/// Retrieves all emails.
		/// </summary>
		public async Task<IEnumerable<Email>> GetAllEmailsAsync()
        {
            return await this.emailRepository.GetAsync();
        }

		/// <summary>
		/// Retrieves one email by Id.
		/// </summary>
		public async Task<Email> GetEmailByIdAsync(Guid id)
        {
            return await this.emailRepository.GetByIdAsync(id);
        }

		/// <summary>
		/// Creates a new email record.
		/// </summary>
        public async Task<Email> CreateEmailAsync(Email email)
        {
            return await this.emailRepository.InsertAsync(email);
        }

		/// <summary>
		/// Updates an existing email record by Id.
		/// </summary>
        public async Task<Email> UpdateEmailAsync(Guid id, Email emailUpdate)
        {
            // Retrieve existing
            Email email = await this.emailRepository.GetByIdAsync(id);
            if (email == null)
            {
                return null;
            }

            // Mapping
            email.Subject = emailUpdate.Subject;
            email.Body = emailUpdate.Body;

            return await this.emailRepository.UpdateAsync(email);
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
		/// Deletes an existing email record by Id.
		/// </summary>
        public async Task<Email> DeleteEmailAsync(Email email)
        {
            await this.emailRepository.DeleteAsync(email);

            return email;
        }
    }
}
