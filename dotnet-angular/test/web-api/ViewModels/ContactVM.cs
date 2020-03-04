using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.API.ViewModels.Identity;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Contact view model
    /// </summary>
    public class ContactVM
    {
		public ContactVM()
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
        [Required]
		public string LastName { get; set; }

		/// <summary>
        /// The AccountId property of Contact.
        /// </summary>
        [Required]
		public Guid AccountId { get; set; }

		/// <summary>
        /// The Id property of Contact.
        /// </summary>
        [Required]
		public Guid Id { get; set; }

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
