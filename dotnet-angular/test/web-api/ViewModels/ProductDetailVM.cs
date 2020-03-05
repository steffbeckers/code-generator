using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.API.ViewModels.Identity;

namespace Test.API.ViewModels
{
	/// <summary>
    /// ProductDetail view model
    /// </summary>
    public class ProductDetailVM
    {
		public ProductDetailVM()
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
        [Required]
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
		public ProductVM Product { get; set; }


		// Generic properties

		/// <summary>
        /// The date and time of when the record is created
        /// </summary>
		public DateTime CreatedOn { get; set; }

		/// <summary>
        /// The date and time of when the record is modified
        /// </summary>
		public DateTime ModifiedOn { get; set; }

    }
}
