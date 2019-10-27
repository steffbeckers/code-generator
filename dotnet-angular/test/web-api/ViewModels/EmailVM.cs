using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Email view model
    /// </summary>
    public class EmailVM
    {
		public EmailVM()
        {
            // Relations

			//// One-to-many
        }

		/// <summary>
        /// The identifier of Email.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Subject property of Email.
        /// </summary>
        [Required]
		public string Subject { get; set; }

		/// <summary>
        /// The Body property of Email.
        /// </summary>
		public string Body { get; set; }

		// Relations

		//// Many-to-one


		//// One-to-many

    }
}
