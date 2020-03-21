using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RJM.API.ViewModels.Identity;

namespace RJM.API.ViewModels
{
	/// <summary>
    /// Document view model
    /// </summary>
    public class DocumentVM
    {
		public DocumentVM()
        {
            // Relations

			//// Many-to-many
			this.Skills = new List<SkillVM>();
        }

		// Properties

		/// <summary>
        /// The identifier of Document.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Document.
        /// </summary>
        [Required]
		public string Name { get; set; }

		/// <summary>
        /// The DisplayName property of Document.
        /// </summary>
		public string DisplayName { get; set; }

		/// <summary>
        /// The Description property of Document.
        /// </summary>
		public string Description { get; set; }

		/// <summary>
        /// The Path property of Document.
        /// </summary>
		public string Path { get; set; }

		/// <summary>
        /// The URL property of Document.
        /// </summary>
		public string URL { get; set; }

		/// <summary>
        /// The MimeType property of Document.
        /// </summary>
		public string MimeType { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key ResumeStateId for ResumeState of Document.
        /// </summary>
		public Guid ResumeStateId { get; set; }

		/// <summary>
        /// The related ResumeState of Document.
        /// </summary>
		public ResumeStateVM ResumeState { get; set; }


		//// Many-to-many

		/// <summary>
        /// The related Skills of Document.
        /// </summary>
		public IList<SkillVM> Skills { get; set; }

        ////// To create a link with Skill directly on create of Document.
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
