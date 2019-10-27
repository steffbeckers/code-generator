using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Notes.
	/// </summary>
    public class NoteBLL
    {
        private readonly NoteRepository noteRepository;
        // TODO: private readonly Note...Repository note...Repository;

		/// <summary>
		/// The constructor of the Note business logic layer.
		/// </summary>
        public NoteBLL(
			NoteRepository noteRepository//,
			// TODO: Note...Repository note...Repository
		)
        {
            this.noteRepository = noteRepository;
            // TODO: this.Note...Repository = Note...Repository;
        }

		/// <summary>
		/// Retrieves all notes.
		/// </summary>
		public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await this.noteRepository.GetAsync();
        }

		/// <summary>
		/// Retrieves one note by Id.
		/// </summary>
		public async Task<Note> GetNoteByIdAsync(Guid id)
        {
            return await this.noteRepository.GetByIdAsync(id);
        }

		/// <summary>
		/// Creates a new note record.
		/// </summary>
        public async Task<Note> CreateNoteAsync(Note note)
        {
            return await this.noteRepository.InsertAsync(note);
        }

		/// <summary>
		/// Updates an existing note record by Id.
		/// </summary>
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

		/// <summary>
		/// Deletes an existing note record by Id.
		/// </summary>
        public async Task<Note> DeleteNoteAsync(Note note)
        {
            await this.noteRepository.DeleteAsync(note);

            return note;
        }
    }
}
