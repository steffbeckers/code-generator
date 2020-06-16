using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RJM.API.ViewModels.Identity;

namespace RJM.API.ViewModels
{
	/// <summary>
    /// SkillAlias view model
    /// </summary>
    public class SkillAliasVM
    {
		public SkillAliasVM()
        {
            // Relations
        }

		// Properties

		/// <summary>
        /// The identifier of SkillAlias.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of SkillAlias.
        /// </summary>
        [Required]
		public string Name { get; set; }

		/// <summary>
        /// The Description property of SkillAlias.
        /// </summary>
		public string Description { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key SkillId for Skill of SkillAlias.
        /// </summary>
		public Guid SkillId { get; set; }

		/// <summary>
        /// The related Skill of SkillAlias.
        /// </summary>
		public SkillVM Skill { get; set; }


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
