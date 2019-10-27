using Test.API.DAL.Repositories;
using Test.API.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Test.API.BLL
{
    public class EmailBLL
    {
        private readonly EmailRepository emailRepository;
        // TODO: private readonly Email...Repository email...Repository;

        public EmailBLL(
			EmailRepository emailRepository//,
			// TODO: Email...Repository email...Repository
		)
        {
            this.emailRepository = emailRepository;
            // TODO: this.Email...Repository = Email...Repository;
        }

		public async Task<IEnumerable<Email>> GetAllEmailsAsync()
        {
            return await this.emailRepository.GetAsync();
        }

		public async Task<Email> GetEmailByIdAsync(Guid id)
        {
            return await this.emailRepository.GetByIdAsync(id);
        }

        public async Task<Email> CreateEmailAsync(Email email)
        {
            return await this.emailRepository.InsertAsync(email);
        }

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

        public async Task<bool> RemoveEmailAsync(Guid id)
        {
            // Retrieve existing
            Email email = await this.emailRepository.GetByIdAsync(id);
            if (email == null)
            {
                return true;
            }

            await this.emailRepository.DeleteAsync(email);

            return true;
        }
    }
}
