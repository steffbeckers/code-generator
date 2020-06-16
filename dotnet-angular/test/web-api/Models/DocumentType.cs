using System;
using System.Collections.Generic;

namespace RJM.API.Models
{
	/// <summary>
    /// DocumentType model
    /// </summary>
    public class DocumentType
    {
		public DocumentType()
        {
            // Relations

			//// One-to-many
			this.Documents = new List<Document>();
        }

		// Properties

		/// <summary>
        /// The identifier of DocumentType.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of DocumentType.
        /// </summary>
		public string Name { get; set; }

		/// <summary>
        /// The DisplayName property of DocumentType.
        /// </summary>
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
		public IList<Document> Documents { get; set; }

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
