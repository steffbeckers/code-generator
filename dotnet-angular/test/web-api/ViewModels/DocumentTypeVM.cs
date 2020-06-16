using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RJM.API.ViewModels.Identity;

namespace RJM.API.ViewModels
{
	/// <summary>
    /// DocumentType view model
    /// </summary>
    public class DocumentTypeVM
    {
		public DocumentTypeVM()
        {
            // Relations

			//// One-to-many
			this.Documents = new List<DocumentVM>();
        }

		// Properties

		/// <summary>
        /// The identifier of DocumentType.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of DocumentType.
        /// </summary>
        [Required]
		public string Name { get; set; }

		/// <summary>
        /// The DisplayName property of DocumentType.
        /// </summary>
        [Required]
		public string DisplayName { get; set; }

		/// <summary>
        /// The Test property of DocumentType.
        /// </summary>
		public string Test { get; set; }

		// Relations

		//// One-to-many

		/// <summary>
        /// The related Documents of DocumentType.
        /// </summary>
		public IList<DocumentVM> Documents { get; set; }

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
