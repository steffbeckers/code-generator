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
        private readonly AccountRepository accountRepository;
        private readonly AccountNoteRepository accountNoteRepository;

		/// <summary>
		/// The constructor of the Note business logic layer.
		/// </summary>
        public NoteBLL(
			NoteRepository noteRepository,
            AccountRepository accountRepository,
			AccountNoteRepository accountNoteRepository
		)
        {
            this.noteRepository = noteRepository;
            this.accountRepository = accountRepository;
			this.accountNoteRepository = accountNoteRepository;
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
            // Validation
            if (note == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(note.Title))
                note.Title = note.Title.Trim();
            if (!string.IsNullOrEmpty(note.Body))
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
        public async Task<Note> UpdateNoteAsync(Note noteUpdate)
        {
            // Validation
            if (noteUpdate == null) { return null; }

            // Retrieve existing
            Note note = await this.noteRepository.GetByIdAsync(noteUpdate.Id);
            if (note == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(noteUpdate.Title))
                noteUpdate.Title = noteUpdate.Title.Trim();
            if (!string.IsNullOrEmpty(noteUpdate.Body))
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

        public async Task<Note> LinkAccountToNoteAsync(AccountNote accountNote)
        {
            // Validation
            if (accountNote == null) { return null; }

            // Check if note exists
            Note note = await this.noteRepository.GetByIdAsync(accountNote.NoteId);
            if (note == null)
            {
                return null;
            }

            // Check if account exists
            Account account = await this.accountRepository.GetByIdAsync(accountNote.AccountId);
            if (account == null)
            {
                return null;
            }

            // Retrieve existing link
            AccountNote accountNoteLink = this.accountNoteRepository.GetByNoteAndAccountId(accountNote.NoteId, accountNote.AccountId);

            if (accountNoteLink == null)
            {
                await this.accountNoteRepository.InsertAsync(accountNote);
            }
            else
            {
                // TODO: Mapping of fields on many-to-many
                //accountNoteLink.Field = accountNote.Field;

                await this.accountNoteRepository.UpdateAsync(accountNoteLink);
            }

            return await this.GetNoteByIdAsync(accountNote.NoteId);
        }

        public async Task<Note> UnlinkAccountFromNoteAsync(AccountNote accountNote)
        {
            // Validation
            if (accountNote == null) { return null; }

            // Retrieve existing link
            AccountNote accountNoteLink = this.accountNoteRepository.GetByNoteAndAccountId(accountNote.NoteId, accountNote.AccountId);
		
            if (accountNoteLink != null)
            {
                await this.accountNoteRepository.DeleteAsync(accountNoteLink);
            }

            return await this.GetNoteByIdAsync(accountNote.NoteId);
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
