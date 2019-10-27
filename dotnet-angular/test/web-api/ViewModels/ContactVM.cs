using System;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Contact view model
    /// </summary>
    public class ContactVM
    {
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
    }
}
