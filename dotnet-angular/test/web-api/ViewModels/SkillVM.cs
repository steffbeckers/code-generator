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

			//// Many-to-many
			this.Resumes = new List<ResumeVM>();
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
        /// The Description property of Skill.
        /// </summary>
		public string Description { get; set; }

		// Relations

		//// Many-to-many

		/// <summary>
        /// The related Resumes of Skill.
        /// </summary>
		public IList<ResumeVM> Resumes { get; set; }

        ////// To create a link with Resume directly on create of Skill.
        public Guid? ResumeId { get; set; }
        public int ResumeRating { get; set; }
        public string ResumeDescription { get; set; }

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
