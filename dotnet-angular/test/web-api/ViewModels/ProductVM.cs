using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.API.ViewModels.Identity;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Product view model
    /// </summary>
    public class ProductVM
    {
		public ProductVM()
        {
            // Relations

			//// Many-to-many
			this.Carts = new List<CartVM>();
        }

		// Properties

		/// <summary>
        /// The identifier of Product.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Product.
        /// </summary>
        [Required]
		public string Name { get; set; }

		/// <summary>
        /// The Description property of Product.
        /// </summary>
		public string Description { get; set; }

		/// <summary>
        /// The Price property of Product.
        /// </summary>
		public double Price { get; set; }

		// Relations

		//// Many-to-many

		/// <summary>
        /// The related Carts of Product.
        /// </summary>
		public IList<CartVM> Carts { get; set; }

        ////// To create a link with Cart directly on create of Product.
        public Guid? CartId { get; set; }
        public int CartQuantity { get; set; }
        public double CartPrice { get; set; }

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
        /// The Id of the user who created the record
        /// </summary>
		public Guid CreatedByUserId { get; set; }

		/// <summary>
        /// The user who created the record
        /// </summary>
		public UserVM CreatedByUser { get; set; }

		/// <summary>
        /// The Id of the user who last modified the record
        /// </summary>
		public Guid ModifiedByUserId { get; set; }

		/// <summary>
        /// The user who last modified the record
        /// </summary>
		public UserVM ModifiedByUser { get; set; }

    }
}
