using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// Order model
    /// </summary>
    public class Order
    {
		public Order()
        {
            // Relations
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

		//// Many-to-one

	    /// <summary>
        /// The related foreign key OrderStateId for OrderState of Order.
        /// </summary>
		public Guid OrderStateId { get; set; }

		/// <summary>
        /// The related OrderState of Order.
        /// </summary>
		public OrderState OrderState { get; set; }


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
        /// The date and time of when the record is (soft) deleted
        /// </summary>
		public DateTime? DeletedOn { get; set; }

		/// <summary>
        /// The Id of the user who created the record
        /// </summary>
		public Guid CreatedByUserId { get; set; }

		/// <summary>
        /// The user who created the record
        /// </summary>
		public User CreatedByUser { get; set; }

		/// <summary>
        /// The Id of the user who last modified the record
        /// </summary>
		public Guid ModifiedByUserId { get; set; }

		/// <summary>
        /// The user who last modified the record
        /// </summary>
		public User ModifiedByUser { get; set; }

		// TODO: Multi-tenancy
		//public Guid TenantId { get; set; }
    }
}
