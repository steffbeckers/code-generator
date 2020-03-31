using System;
using System.Collections.Generic;

namespace RJM.API.Models
{
	/// <summary>
    /// DocumentResume model
    /// </summary>
    public class DocumentResume
    {
		public DocumentResume()
        {
            // Relations
        }

		// Properties

		/// <summary>
        /// The identifier of DocumentResume.
        /// </summary>
		public Guid Id { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key DocumentId for Document of DocumentResume.
        /// </summary>
		public Guid DocumentId { get; set; }

		/// <summary>
        /// The related Document of DocumentResume.
        /// </summary>
		public Document Document { get; set; }

	    /// <summary>
        /// The related foreign key ResumeId for Resume of DocumentResume.
        /// </summary>
		public Guid ResumeId { get; set; }

		/// <summary>
        /// The related Resume of DocumentResume.
        /// </summary>
		public Resume Resume { get; set; }


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

		/// <summary>
        /// The Id of the user who created the record
        /// </summary>
		public Guid CreatedByUserId { get; set; }

		/// <summary>
        /// The user who created the record
        /// </summary>
		public User CreatedByUser { get; set; }

		/// <summary>
        /// The Id of the user who last modified the record
        /// </summary>
		public Guid ModifiedByUserId { get; set; }

		/// <summary>
        /// The user who last modified the record
        /// </summary>
		public User ModifiedByUser { get; set; }

		// TODO: Multi-tenancy
		//public Guid TenantId { get; set; }
    }
}
