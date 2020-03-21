using System;
using System.Collections.Generic;

namespace RJM.API.Models
{
	/// <summary>
    /// JobSkill model
    /// </summary>
    public class JobSkill
    {
		public JobSkill()
        {
            // Relations
        }

		// Properties

		/// <summary>
        /// The identifier of JobSkill.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Rating property of JobSkill.
        /// </summary>
		public int? Rating { get; set; }

		/// <summary>
        /// The Description property of JobSkill.
        /// </summary>
		public string Description { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key JobId for Job of JobSkill.
        /// </summary>
		public Guid JobId { get; set; }

		/// <summary>
        /// The related Job of JobSkill.
        /// </summary>
		public Job Job { get; set; }

	    /// <summary>
        /// The related foreign key SkillId for Skill of JobSkill.
        /// </summary>
		public Guid SkillId { get; set; }

		/// <summary>
        /// The related Skill of JobSkill.
        /// </summary>
		public Skill Skill { get; set; }


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
