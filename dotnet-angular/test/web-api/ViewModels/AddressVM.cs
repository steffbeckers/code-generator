using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.API.ViewModels.Identity;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Address view model
    /// </summary>
    public class AddressVM
    {
		public AddressVM()
        {
        }

		// Properties

		/// <summary>
        /// The identifier of Address.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Street property of Address.
        /// </summary>
        [Required]
		public string Street { get; set; }

		/// <summary>
        /// The Number property of Address.
        /// </summary>
        [Required]
		public string Number { get; set; }

		/// <summary>
        /// The PostalCode property of Address.
        /// </summary>
        [Required]
		public string PostalCode { get; set; }

		/// <summary>
        /// The City property of Address.
        /// </summary>
        [Required]
		public string City { get; set; }

		/// <summary>
        /// The Primary property of Address.
        /// </summary>
		public bool Primary { get; set; }


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
