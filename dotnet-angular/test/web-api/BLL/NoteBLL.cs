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
        private readonly ProjectNoteRepository projectNoteRepository;

        /// <summary>
        /// The constructor of the Note business logic layer.
        /// </summary>
        public NoteBLL(
            NoteRepository noteRepository,
            ProjectNoteRepository projectNoteRepository
        )
        {
            this.noteRepository = noteRepository;
            this.projectNoteRepository = projectNoteRepository;
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

        public async Task<Note> LinkProjectToNoteAsync(ProjectNote projectNote)
        {
            ProjectNote projectNoteLink = this.projectNoteRepository.GetByNoteAndProjectId(projectNote.NoteId, projectNote.ProjectId);

            if (projectNoteLink == null)
            {
                await this.projectNoteRepository.InsertAsync(projectNote);
            }
            else
            {
                // TODO: Mapping of fields on many-to-many
                //projectNoteLink.Field = projectNote.Field;

                await this.projectNoteRepository.UpdateAsync(projectNoteLink);
            }

            return await this.GetNoteByIdAsync(projectNote.NoteId);
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
        /// Deletes an existing note record by Id.
        /// </summary>
        public async Task<Note> DeleteNoteAsync(Note note)
        {
            await this.noteRepository.DeleteAsync(note);

            return note;
        }
    }
}
