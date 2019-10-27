using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Calls.
	/// </summary>
    public class CallBLL
    {
        private readonly CallRepository callRepository;

		/// <summary>
		/// The constructor of the Call business logic layer.
		/// </summary>
        public CallBLL(
			CallRepository callRepository
		)
        {
            this.callRepository = callRepository;
        }

		/// <summary>
		/// Retrieves all calls.
		/// </summary>
		public async Task<IEnumerable<Call>> GetAllCallsAsync()
        {
            return await this.callRepository.GetAsync();
        }

		/// <summary>
		/// Retrieves one call by Id.
		/// </summary>
		public async Task<Call> GetCallByIdAsync(Guid id)
        {
            return await this.callRepository.GetByIdAsync(id);
        }

		/// <summary>
		/// Creates a new call record.
		/// </summary>
        public async Task<Call> CreateCallAsync(Call call)
        {
            return await this.callRepository.InsertAsync(call);
        }

		/// <summary>
		/// Updates an existing call record by Id.
		/// </summary>
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
		/// Deletes an existing call record by Id.
		/// </summary>
        public async Task<Call> DeleteCallAsync(Call call)
        {
            await this.callRepository.DeleteAsync(call);

            return call;
        }
    }
}
