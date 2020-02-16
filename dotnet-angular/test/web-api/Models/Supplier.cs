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

		/// <summary>
        /// The Id of the user who created the record
        /// </summary>
		public Guid? CreatedByUserId { get; set; }

		/// <summary>
        /// The user who created the record
        /// </summary>
		public User CreatedByUser { get; set; }

		/// <summary>
        /// The Id of the user who last modified the record
        /// </summary>
		public Guid? ModifiedByUserId { get; set; }

		/// <summary>
        /// The user who last modified the record
        /// </summary>
		public User ModifiedByUser { get; set; }

		// TODO: Multi-tenancy
		//public Guid TenantId { get; set; }
    }
}
