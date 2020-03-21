using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RJM.API.ViewModels.Identity;

namespace RJM.API.ViewModels
{
	/// <summary>
    /// ResumeState view model
    /// </summary>
    public class ResumeStateVM
    {
		public ResumeStateVM()
        {
            // Relations

			//// One-to-many
			this.Resumes = new List<ResumeVM>();
        }

		// Properties

		/// <summary>
        /// The identifier of ResumeState.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of ResumeState.
        /// </summary>
        [Required]
		public string Name { get; set; }

		/// <summary>
        /// The DisplayName property of ResumeState.
        /// </summary>
        [Required]
		public string DisplayName { get; set; }

		// Relations

		//// One-to-many

		/// <summary>
        /// The related Resumes of ResumeState.
        /// </summary>
		public IList<ResumeVM> Resumes { get; set; }

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
        /// The Id of the user who created the record
        /// </summary>
		public Guid CreatedByUserId { get; set; }

		/// <summary>
        /// The user who created the record
        /// </summary>
		public UserVM CreatedByUser { get; set; }

		/// <summary>
        /// The Id of the user who last modified the record
        /// </summary>
		public Guid ModifiedByUserId { get; set; }

		/// <summary>
        /// The user who last modified the record
        /// </summary>
		public UserVM ModifiedByUser { get; set; }

    }
}
