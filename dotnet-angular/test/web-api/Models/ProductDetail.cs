using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// ProductDetail model
    /// </summary>
    public class ProductDetail
    {
		public ProductDetail()
        {
            // Relations
        }

		// Properties

		/// <summary>
        /// The identifier of ProductDetail.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Comment property of ProductDetail.
        /// </summary>
		public string Comment { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key ProductId for Product of ProductDetail.
        /// </summary>
		public Guid ProductId { get; set; }

		/// <summary>
        /// The related Product of ProductDetail.
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

		// TODO:
		//public Guid CreatedByUserId { get; set; }
		//public Guid ModifiedByUserId { get; set; }
		//public Guid TenantId { get; set; }
    }
}
