using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// ProductSupplier model
    /// </summary>
    public class ProductSupplier
    {
		public ProductSupplier()
        {
            // Relations
        }

		// Properties

		/// <summary>
        /// The identifier of ProductSupplier.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Comment property of ProductSupplier.
        /// </summary>
		public string Comment { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key ProductId for Product of ProductSupplier.
        /// </summary>
		public Guid ProductId { get; set; }

		/// <summary>
        /// The related Product of ProductSupplier.
        /// </summary>
		public Product Product { get; set; }

	    /// <summary>
        /// The related foreign key SupplierId for Supplier of ProductSupplier.
        /// </summary>
		public Guid SupplierId { get; set; }

		/// <summary>
        /// The related Supplier of ProductSupplier.
        /// </summary>
		public Supplier Supplier { get; set; }


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

		// TODO
		//public Guid TenantId { get; set; }
    }
}
