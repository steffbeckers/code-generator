using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.API.ViewModels.Identity;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Order view model
    /// </summary>
    public class OrderVM
    {
		public OrderVM()
        {
            // Relations

			//// One-to-many
			this.OrderState = new List<OrderStateVM>();
        }

		// Properties

		/// <summary>
        /// The identifier of Order.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Number property of Order.
        /// </summary>
		public string Number { get; set; }

		/// <summary>
        /// The Description property of Order.
        /// </summary>
		public string Description { get; set; }

		/// <summary>
        /// The TotalPrice property of Order.
        /// </summary>
		public double TotalPrice { get; set; }

		// Relations

		//// One-to-many

		/// <summary>
        /// The related OrderState of Order.
        /// </summary>
		public IList<OrderStateVM> OrderState { get; set; }

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