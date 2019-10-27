using Test.API.DAL.Repositories;
using Test.API.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Test.API.BLL
{
    public class CallBLL
    {
        private readonly CallRepository callRepository;
        // TODO: private readonly Call...Repository call...Repository;

        public CallBLL(
			CallRepository callRepository//,
			// TODO: Call...Repository call...Repository
		)
        {
            this.callRepository = callRepository;
            // TODO: this.Call...Repository = Call...Repository;
        }

		public async Task<IEnumerable<Call>> GetAllCalls()
        {
            return await this.callRepository.GetAsync();
        }

		public async Task<Call> GetCallById(Guid id)
        {
            return await this.callRepository.GetByIdAsync(id);
        }

        public async Task<Call> CreateCallAsync(Call call)
        {
            return await this.callRepository.InsertAsync(call);
        }

        public async Task<Call> UpdateCallAsync(Guid id, Call callUpdate)
        {
            // Retrieve existing
            Call call = await this.callRepository.GetByIdAsync(id);
            if (call == null)
            {
                return null;
            }

            // Mapping
            call.Date = callUpdate.Date;

            return await this.callRepository.UpdateAsync(call);
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

        public async Task<bool> RemoveCallAsync(Guid id)
        {
            // Retrieve existing
            Call call = await this.callRepository.GetByIdAsync(id);
            if (call == null)
            {
                return true;
            }

            await this.callRepository.DeleteAsync(call);

            return true;
        }
    }
}
