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

		//// Many-to-one

	    /// <summary>
        /// The related foreign key ParentAccountId for ParentAccount of Account.
        /// </summary>
		public Guid? ParentAccountId { get; set; }

		/// <summary>
        /// The related ParentAccount of Account.
        /// </summary>
		public Account ParentAccount { get; set; }


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
