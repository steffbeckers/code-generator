using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Account view model
    /// </summary>
    public class AccountVM
    {
		public AccountVM()
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
        [Required]
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
		public Guid? AccountId { get; set; }

		/// <summary>
        /// The related ParentAccount of Account.
        /// </summary>
		public AccountVM ParentAccount { get; set; }

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
		public User CreatedByUser { get; set; }

		/// <summary>
        /// The Id of the user who last modified the record
        /// </summary>
		public Guid ModifiedByUserId { get; set; }

		/// <summary>
        /// The user who last modified the record
        /// </summary>
		public User ModifiedByUser { get; set; }

    }
}
