using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.API.ViewModels.Identity;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Account view model
    /// </summary>
    public class AccountVM
    {
		public AccountVM()
        {
        }

		// Properties

		/// <summary>
        /// The identifier of Account.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Id property of Account.
        /// </summary>
        [Required]
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Account.
        /// </summary>
        [Required]
		public string Name { get; set; }

		/// <summary>
        /// The Email property of Account.
        /// </summary>
		public string Email { get; set; }

		/// <summary>
        /// The Telephone property of Account.
        /// </summary>
		public string Telephone { get; set; }

		/// <summary>
        /// The Fax property of Account.
        /// </summary>
		public string Fax { get; set; }

		/// <summary>
        /// The Website property of Account.
        /// </summary>
		public string Website { get; set; }

		/// <summary>
        /// The VATNumber property of Account.
        /// </summary>
		public string VATNumber { get; set; }

		/// <summary>
        /// The Description property of Account.
        /// </summary>
		public string Description { get; set; }

		/// <summary>
        /// The AddressId property of Account.
        /// </summary>
		public Guid? AddressId { get; set; }

		/// <summary>
        /// The ParentAccountId property of Account.
        /// </summary>
		public Guid? ParentAccountId { get; set; }

		/// <summary>
        /// The BillingAccountId property of Account.
        /// </summary>
		public Guid? BillingAccountId { get; set; }

		/// <summary>
        /// The RelationTypeId property of Account.
        /// </summary>
		public Guid? RelationTypeId { get; set; }

		/// <summary>
        /// The PrimaryContactId property of Account.
        /// </summary>
		public Guid? PrimaryContactId { get; set; }


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
