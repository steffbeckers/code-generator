using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// Supplier model
    /// </summary>
    public class Supplier
    {
		public Supplier()
        {
            // Relations

			//// Many-to-many
			this.ProductSupplier = new List<ProductSupplier>();
        }

		// Properties

		/// <summary>
        /// The identifier of Supplier.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Supplier.
        /// </summary>
		public string Name { get; set; }

		/// <summary>
        /// The Phone property of Supplier.
        /// </summary>
		public string Phone { get; set; }

		// Relations

		//// Many-to-many

		/// <summary>
        /// The related Products of Supplier.
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