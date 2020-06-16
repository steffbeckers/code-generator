using System;
using System.Collections.Generic;

namespace RJM.API.Models
{
	/// <summary>
    /// ResumeState model
    /// </summary>
    public class ResumeState
    {
		public ResumeState()
        {
            // Relations

			//// One-to-many
			this.Resumes = new List<Resume>();
        }

		// Properties

		/// <summary>
        /// The identifier of ResumeState.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of ResumeState.
        /// </summary>
		public string Name { get; set; }

		/// <summary>
        /// The DisplayName property of ResumeState.
        /// </summary>
		public string DisplayName { get; set; }

		// Relations

		//// One-to-many

		/// <summary>
        /// The related Resumes of ResumeState.
        /// </summary>
		public IList<Resume> Resumes { get; set; }

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
