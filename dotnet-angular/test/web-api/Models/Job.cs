using System;
using System.Collections.Generic;

namespace RJM.API.Models
{
	/// <summary>
    /// Job model
    /// </summary>
    public class Job
    {
		public Job()
        {
            // Relations

			//// Many-to-many
			this.JobSkill = new List<JobSkill>();
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
		public JobState JobState { get; set; }


		//// Many-to-many

		/// <summary>
        /// The related Skills of Job.
        /// </summary>
		public IList<JobSkill> JobSkill { get; set; }

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
