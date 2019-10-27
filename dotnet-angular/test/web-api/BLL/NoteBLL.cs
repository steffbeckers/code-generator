using Test.API.DAL.Repositories;
using Test.API.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Test.API.BLL
{
    public class NoteBLL
    {
        private readonly NoteRepository noteRepository;
        // TODO: private readonly Note...Repository note...Repository;

        public NoteBLL(
			NoteRepository noteRepository//,
			// TODO: Note...Repository note...Repository
		)
        {
            this.noteRepository = noteRepository;
            // TODO: this.Note...Repository = Note...Repository;
        }

		public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await this.noteRepository.GetAsync();
        }

		public async Task<Note> GetNoteByIdAsync(Guid id)
        {
            return await this.noteRepository.GetByIdAsync(id);
        }

        public async Task<Note> CreateNoteAsync(Note note)
        {
            return await this.noteRepository.InsertAsync(note);
        }

        public async Task<Note> UpdateNoteAsync(Guid id, Note noteUpdate)
        {
            // Retrieve existing
            Note note = await this.noteRepository.GetByIdAsync(id);
            if (note == null)
            {
                return null;
            }

            // Mapping
            note.Title = noteUpdate.Title;
            note.Body = noteUpdate.Body;

            return await this.noteRepository.UpdateAsync(note);
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

        public async Task<bool> RemoveNoteAsync(Guid id)
        {
            // Retrieve existing
            Note note = await this.noteRepository.GetByIdAsync(id);
            if (note == null)
            {
                return true;
            }

            await this.noteRepository.DeleteAsync(note);

            return true;
        }
    }
}
