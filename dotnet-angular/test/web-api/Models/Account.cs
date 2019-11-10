using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// Account model
    /// </summary>
    public class Account
    {
		public Account()
        {
            // Relations

			//// One-to-many
			this.Addresses = new List<Address>();
			this.Contacts = new List<Contact>();

			//// Many-to-many
			this.AccountNote = new List<AccountNote>();
        }

		// Properties

		/// <summary>
        /// The identifier of Account.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Account.
        /// </summary>
		public string Name { get; set; }

		/// <summary>
        /// The Website property of Account.
        /// </summary>
		public string Website { get; set; }

		/// <summary>
        /// The Telephone property of Account.
        /// </summary>
		public string Telephone { get; set; }

		/// <summary>
        /// The Email property of Account.
        /// </summary>
		public string Email { get; set; }

		// Relations

		//// One-to-many

		/// <summary>
        /// The related Addresses of Account.
        /// </summary>
		public IList<Address> Addresses { get; set; }
		/// <summary>
        /// The related Contacts of Account.
        /// </summary>
		public IList<Contact> Contacts { get; set; }

		//// Many-to-many

		/// <summary>
        /// The related Notes of Account.
        /// </summary>
		public IList<AccountNote> AccountNote { get; set; }

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
