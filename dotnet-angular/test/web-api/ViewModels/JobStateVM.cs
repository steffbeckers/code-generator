using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RJM.API.ViewModels.Identity;

namespace RJM.API.ViewModels
{
	/// <summary>
    /// JobState view model
    /// </summary>
    public class JobStateVM
    {
		public JobStateVM()
        {
            // Relations

			//// One-to-many
			this.Jobs = new List<JobVM>();
        }

		// Properties

		/// <summary>
        /// The identifier of JobState.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of JobState.
        /// </summary>
        [Required]
		public string Name { get; set; }

		/// <summary>
        /// The DisplayName property of JobState.
        /// </summary>
        [Required]
		public string DisplayName { get; set; }

		// Relations

		//// One-to-many

		/// <summary>
        /// The related Jobs of JobState.
        /// </summary>
		public IList<JobVM> Jobs { get; set; }

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
