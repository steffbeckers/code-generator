using System;
using System.Collections.Generic;

namespace RJM.API.Models
{
	/// <summary>
    /// Document model
    /// </summary>
    public class Document
    {
		public Document()
        {
        }

		// Properties

		/// <summary>
        /// The identifier of Document.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Document.
        /// </summary>
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
