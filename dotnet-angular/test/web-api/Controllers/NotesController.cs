using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.BLL;
using Test.API.Models;
using Test.API.ViewModels;

namespace Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly ILogger<NotesController> logger;
        private readonly IMapper mapper;
        private readonly NoteBLL bll;

        public NotesController(
            ILogger<NotesController> logger,
			IMapper mapper,
            NoteBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Notes
		/// <summary>
		/// Retrieves all notes.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteVM>>> GetNotes()
        {
            IEnumerable<Note> notes = await this.bll.GetAllNotesAsync();

            return this.mapper.Map<IEnumerable<Note>, List<NoteVM>>(notes);
        }

        // GET: api/Notes/{id}
		/// <summary>
		/// Retrieves a specific note.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteVM>> GetNote([FromRoute] Guid id)
        {
            Note note = await this.bll.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Note, NoteVM>(note);
        }

        // POST: api/Notes
		/// <summary>
		/// Creates a new note.
		/// </summary>
		/// <param name="noteVM"></param>
        [HttpPost]
        public async Task<ActionResult<NoteVM>> CreateNote([FromBody] NoteVM noteVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Note note = this.mapper.Map<NoteVM, Note>(noteVM);

            note = await this.bll.CreateNoteAsync(note);

            return CreatedAtAction(
				"GetNote",
				new { id = note.Id },
				this.mapper.Map<Note, NoteVM>(note)
			);
        }

		// PUT: api/Notes/{id}
		/// <summary>
		/// Updates a specific note.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="noteVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<NoteVM>> UpdateNote([FromRoute] Guid id, [FromBody] NoteVM noteVM)
        {
			// Validation
            if (!ModelState.IsValid || id != noteVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing note
            Note note = await this.bll.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

			// Mapping
            Note noteUpdate = this.mapper.Map<NoteVM, Note>(noteVM);

			// Update fields
            note.Title = noteUpdate.Title;
            note.Body = noteUpdate.Body;
			
            note = await this.bll.UpdateNoteAsync(id, note);

			return this.mapper.Map<Note, NoteVM>(note);
        }

        // DELETE: api/Notes/{id}
		/// <summary>
		/// Deletes a specific note.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<NoteVM>> DeleteNote([FromRoute] Guid id)
        {
            // Retrieve existing note
            Note note = await this.bll.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            await this.bll.RemoveNoteAsync(id);

            return this.mapper.Map<Note, NoteVM>(note);
        }
    }
}
