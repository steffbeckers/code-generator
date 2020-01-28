using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// Product model
    /// </summary>
    public class Product
    {
		public Product()
        {
            // Relations

			//// One-to-many
			this.Details = new List<ProductDetail>();

			//// Many-to-many
			this.ProductSupplier = new List<ProductSupplier>();
        }

		// Properties

		/// <summary>
        /// The identifier of Product.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Product.
        /// </summary>
		public string Name { get; set; }

		/// <summary>
        /// The Code property of Product.
        /// </summary>
		public string Code { get; set; }

		/// <summary>
        /// The Quantity property of Product.
        /// </summary>
		public int? Quantity { get; set; }

		/// <summary>
        /// The Price property of Product.
        /// </summary>
		public double Price { get; set; }

		// Relations

		//// One-to-many

		/// <summary>
        /// The related Details of Product.
        /// </summary>
		public IList<ProductDetail> Details { get; set; }

		//// Many-to-many

		/// <summary>
        /// The related Suppliers of Product.
        /// </summary>
		public IList<ProductSupplier> ProductSupplier { get; set; }

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

		// TODO:
		//public Guid CreatedByUserId { get; set; }
		//public Guid ModifiedByUserId { get; set; }
		//public Guid TenantId { get; set; }
    }
}
