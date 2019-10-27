using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        /// The FirstName property of Contact.
        /// </summary>
        [Required]
		public string FirstName { get; set; }

		/// <summary>
        /// The LastName property of Contact.
        /// </summary>
        [Required]
		public string LastName { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key AccountId for Account of Contact.
        /// </summary>
		public Guid? AccountId { get; set; }

		/// <summary>
        /// The related Account of Contact.
        /// </summary>
		public AccountVM Account { get; set; }
    }
}
