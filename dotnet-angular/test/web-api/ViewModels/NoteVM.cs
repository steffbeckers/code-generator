using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Note view model
    /// </summary>
    public class NoteVM
    {
		public NoteVM()
        {
        }

		// Properties

		/// <summary>
        /// The identifier of Note.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Title property of Note.
        /// </summary>
        [Required]
		public string Title { get; set; }

		/// <summary>
        /// The Body property of Note.
        /// </summary>
		public string Body { get; set; }

    }
}
