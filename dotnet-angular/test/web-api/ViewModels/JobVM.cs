using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RJM.API.ViewModels.Identity;

namespace RJM.API.ViewModels
{
	/// <summary>
    /// Job view model
    /// </summary>
    public class JobVM
    {
		public JobVM()
        {
            // Relations

			//// Many-to-many
			this.Skills = new List<SkillVM>();
        }

		// Properties

		/// <summary>
        /// The identifier of Job.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Title property of Job.
        /// </summary>
		public string Title { get; set; }

		/// <summary>
        /// The Description property of Job.
        /// </summary>
		public string Description { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key JobStateId for JobState of Job.
        /// </summary>
		public Guid JobStateId { get; set; }

		/// <summary>
        /// The related JobState of Job.
        /// </summary>
		public JobStateVM JobState { get; set; }


		//// Many-to-many

		/// <summary>
        /// The related Skills of Job.
        /// </summary>
		public IList<SkillVM> Skills { get; set; }

        ////// To create a link with Skill directly on create of Job.
        public Guid? SkillId { get; set; }
        public int? SkillLevel { get; set; }
        public string SkillDescription { get; set; }

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
