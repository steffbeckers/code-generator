using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RJM.API.ViewModels.Identity;

namespace RJM.API.ViewModels
{
	/// <summary>
    /// Skill view model
    /// </summary>
    public class SkillVM
    {
		public SkillVM()
        {
            // Relations

			//// One-to-many
			this.Aliases = new List<SkillAliasVM>();

			//// Many-to-many
			this.Resumes = new List<ResumeVM>();
			this.Jobs = new List<JobVM>();
        }

		// Properties

		/// <summary>
        /// The identifier of Skill.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Skill.
        /// </summary>
        [Required]
		public string Name { get; set; }

		/// <summary>
        /// The DisplayName property of Skill.
        /// </summary>
        [Required]
		public string DisplayName { get; set; }

		/// <summary>
        /// The Description property of Skill.
        /// </summary>
		public string Description { get; set; }

		// Relations

		//// One-to-many

		/// <summary>
        /// The related Aliases of Skill.
        /// </summary>
		public IList<SkillAliasVM> Aliases { get; set; }

		//// Many-to-many

		/// <summary>
        /// The related Resumes of Skill.
        /// </summary>
		public IList<ResumeVM> Resumes { get; set; }

        ////// To create a link with Resume directly on create of Skill.
        public Guid? ResumeId { get; set; }
        public int? ResumeLevel { get; set; }
        public string ResumeDescription { get; set; }
		/// <summary>
        /// The related Jobs of Skill.
        /// </summary>
		public IList<JobVM> Jobs { get; set; }

        ////// To create a link with Job directly on create of Skill.
        public Guid? JobId { get; set; }
        public int? JobLevel { get; set; }
        public string JobDescription { get; set; }

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
