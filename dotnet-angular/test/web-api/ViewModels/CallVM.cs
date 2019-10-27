using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Call view model
    /// </summary>
    public class CallVM
    {
		public CallVM()
        {
            // Relations

			//// One-to-many
        }

		/// <summary>
        /// The identifier of Call.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Date property of Call.
        /// </summary>
        [Required]
		public DateTime Date { get; set; }

		// Relations

		//// Many-to-one


		//// One-to-many

    }
}
