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

		/// <summary>
		/// The constructor of the Note business logic layer.
		/// </summary>
        public NoteBLL(
			NoteRepository noteRepository
		)
        {
            this.noteRepository = noteRepository;
        }

		/// <summary>
		/// Retrieves all notes.
		/// </summary>
		public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.noteRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one note by Id.
		/// </summary>
		public async Task<Note> GetNoteByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.noteRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new note record.
		/// </summary>
        public async Task<Note> CreateNoteAsync(Note note)
        {
			// Trimming strings
            note.Title = note.Title.Trim();
            note.Body = note.Body.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			note = await this.noteRepository.InsertAsync(note);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return note;
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

			// Trimming strings
            noteUpdate.Title = noteUpdate.Title.Trim();
            noteUpdate.Body = noteUpdate.Body.Trim();

            // Mapping
            note.Title = noteUpdate.Title;
            note.Body = noteUpdate.Body;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			note = await this.noteRepository.UpdateAsync(note);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return note;
        }

		/// <summary>
		/// Deletes an existing note record by Id.
		/// </summary>
        public async Task<Note> DeleteNoteAsync(Note note)
        {
			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.noteRepository.DeleteAsync(note);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return note;
        }
    }
}
