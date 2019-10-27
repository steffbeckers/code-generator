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
            // Relations

			//// One-to-many
        }

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
        /// The related Account of Contact.
        /// </summary>
		public IList<AccountVM> Account { get; set; }

		//// One-to-many

    }
}
