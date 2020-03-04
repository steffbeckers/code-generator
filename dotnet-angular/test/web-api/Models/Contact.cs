using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// Contact model
    /// </summary>
    public class Contact
    {
		public Contact()
        {
        }

		// Properties

		/// <summary>
        /// The identifier of Contact.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The LastName property of Contact.
        /// </summary>
		public string LastName { get; set; }

		/// <summary>
        /// The AccountId property of Contact.
        /// </summary>
		public Guid AccountId { get; set; }

		/// <summary>
        /// The FirstName property of Contact.
        /// </summary>
		public string FirstName { get; set; }

		/// <summary>
        /// The JobTitle property of Contact.
        /// </summary>
		public string JobTitle { get; set; }

		/// <summary>
        /// The Email property of Contact.
        /// </summary>
		public string Email { get; set; }

		/// <summary>
        /// The Telephone property of Contact.
        /// </summary>
		public string Telephone { get; set; }

		/// <summary>
        /// The MobilePhone property of Contact.
        /// </summary>
		public string MobilePhone { get; set; }

		/// <summary>
        /// The Gender property of Contact.
        /// </summary>
		public string Gender { get; set; }


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
