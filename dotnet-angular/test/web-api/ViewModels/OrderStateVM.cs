using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.API.ViewModels.Identity;

namespace Test.API.ViewModels
{
	/// <summary>
    /// OrderState view model
    /// </summary>
    public class OrderStateVM
    {
		public OrderStateVM()
        {
            // Relations

			//// One-to-many
			this.Orders = new List<OrderVM>();
        }

		// Properties

		/// <summary>
        /// The identifier of OrderState.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of OrderState.
        /// </summary>
        [Required]
		public string Name { get; set; }

		/// <summary>
        /// The DisplayName property of OrderState.
        /// </summary>
		public string DisplayName { get; set; }

		// Relations

		//// One-to-many

		/// <summary>
        /// The related Orders of OrderState.
        /// </summary>
		public IList<OrderVM> Orders { get; set; }

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
