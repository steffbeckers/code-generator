using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// AccountNote model
    /// </summary>
    public class AccountNote
    {
		public AccountNote()
        {
            // Relations
        }

		// Properties

		/// <summary>
        /// The identifier of AccountNote.
        /// </summary>
		public Guid Id { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key AccountId for Account of AccountNote.
        /// </summary>
		public Guid AccountId { get; set; }

		/// <summary>
        /// The related Account of AccountNote.
        /// </summary>
		public Account Account { get; set; }

	    /// <summary>
        /// The related foreign key NoteId for Note of AccountNote.
        /// </summary>
		public Guid NoteId { get; set; }

		/// <summary>
        /// The related Note of AccountNote.
        /// </summary>
		public Note Note { get; set; }


		// Generic properties

		/// <summary>
        /// The date and time of when the record is created
        /// </summary>
		public DateTime CreatedOn { get; set; }

		/// <summary>
        /// The date and time of when the record is modified
        /// </summary>
		public DateTime ModifiedOn { get; set; }

		/// <summary>
        /// The date and time of when the record is (soft) deleted
        /// </summary>
		public DateTime? DeletedOn { get; set; }

		// TODO:
		//public Guid CreatedByUserId { get; set; }
		//public Guid ModifiedByUserId { get; set; }
		//public Guid TenantId { get; set; }
    }
}
