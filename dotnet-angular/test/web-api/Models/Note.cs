using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// Note model
    /// </summary>
    public class Note
    {
		public Note()
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
		public string Title { get; set; }

		/// <summary>
        /// The Body property of Note.
        /// </summary>
		public string Body { get; set; }


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
