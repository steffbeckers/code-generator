using System;

namespace Test.API.Models
{
	/// <summary>
    /// Contact model
    /// </summary>
    public class Contact
    {
		/// <summary>
        /// The identifier of Contact.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The FirstName property of Contact.
        /// </summary>
		public string FirstName { get; set; }

		/// <summary>
        /// The LastName property of Contact.
        /// </summary>
		public string LastName { get; set; }

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

		// TODO:
		//public Guid CreatedByUserId { get; set; }
		//public Guid ModifiedByUserId { get; set; }
		//public Guid TenantId { get; set; }
    }
}
