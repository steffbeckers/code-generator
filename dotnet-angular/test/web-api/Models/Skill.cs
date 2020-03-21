using System;
using System.Collections.Generic;

namespace RJM.API.Models
{
	/// <summary>
    /// Skill model
    /// </summary>
    public class Skill
    {
		public Skill()
        {
            // Relations

			//// Many-to-many
			this.ResumeSkill = new List<ResumeSkill>();
        }

		// Properties

		/// <summary>
        /// The identifier of Skill.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Skill.
        /// </summary>
		public string Name { get; set; }

		/// <summary>
        /// The Description property of Skill.
        /// </summary>
		public string Description { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key AliasesId for Aliases of Skill.
        /// </summary>
		public Guid? AliasesId { get; set; }

		/// <summary>
        /// The related Aliases of Skill.
        /// </summary>
		public SkillAlias Aliases { get; set; }


		//// Many-to-many

		/// <summary>
        /// The related Resumes of Skill.
        /// </summary>
		public IList<ResumeSkill> ResumeSkill { get; set; }

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
