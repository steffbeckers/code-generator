using System;
using System.Collections.Generic;

namespace RJM.API.Models
{
	/// <summary>
    /// ResumeSkill model
    /// </summary>
    public class ResumeSkill
    {
		public ResumeSkill()
        {
            // Relations
        }

		// Properties

		/// <summary>
        /// The identifier of ResumeSkill.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Rating property of ResumeSkill.
        /// </summary>
		public int Rating { get; set; }

		/// <summary>
        /// The Description property of ResumeSkill.
        /// </summary>
		public string Description { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key ResumeId for Resume of ResumeSkill.
        /// </summary>
		public Guid ResumeId { get; set; }

		/// <summary>
        /// The related Resume of ResumeSkill.
        /// </summary>
		public Resume Resume { get; set; }

	    /// <summary>
        /// The related foreign key SkillId for Skill of ResumeSkill.
        /// </summary>
		public Guid SkillId { get; set; }

		/// <summary>
        /// The related Skill of ResumeSkill.
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
