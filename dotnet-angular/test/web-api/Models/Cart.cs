using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// Cart model
    /// </summary>
    public class Cart
    {
		public Cart()
        {
            // Relations

			//// Many-to-many
			this.CartProduct = new List<CartProduct>();
        }

		// Properties

		/// <summary>
        /// The identifier of Cart.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Cart.
        /// </summary>
		public string Name { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key UserId for User of Cart.
        /// </summary>
		public Guid? UserId { get; set; }

		/// <summary>
        /// The related User of Cart.
        /// </summary>
		public User User { get; set; }


		//// Many-to-many

		/// <summary>
        /// The related Products of Cart.
        /// </summary>
		public IList<CartProduct> CartProduct { get; set; }

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
