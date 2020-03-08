using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// CartProduct model
    /// </summary>
    public class CartProduct
    {
		public CartProduct()
        {
            // Relations
        }

		// Properties

		/// <summary>
        /// The identifier of CartProduct.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Quantity property of CartProduct.
        /// </summary>
		public int Quantity { get; set; }

		/// <summary>
        /// The Price property of CartProduct.
        /// </summary>
		public double Price { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key CartId for Cart of CartProduct.
        /// </summary>
		public Guid CartId { get; set; }

		/// <summary>
        /// The related Cart of CartProduct.
        /// </summary>
		public Cart Cart { get; set; }

	    /// <summary>
        /// The related foreign key ProductId for Product of CartProduct.
        /// </summary>
		public Guid ProductId { get; set; }

		/// <summary>
        /// The related Product of CartProduct.
        /// </summary>
		public Product Product { get; set; }


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