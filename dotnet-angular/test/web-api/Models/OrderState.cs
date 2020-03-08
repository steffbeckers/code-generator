using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// OrderState model
    /// </summary>
    public class OrderState
    {
		public OrderState()
        {
            // Relations
        }

		// Properties

		/// <summary>
        /// The identifier of OrderState.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of OrderState.
        /// </summary>
		public string Name { get; set; }

		/// <summary>
        /// The DisplayName property of OrderState.
        /// </summary>
		public string DisplayName { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key OrderId for Order of OrderState.
        /// </summary>
		public Guid OrderId { get; set; }

		/// <summary>
        /// The related Order of OrderState.
        /// </summary>
		public Order Order { get; set; }


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
