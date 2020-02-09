using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

			//// One-to-many
			this.Details = new List<ProductDetailVM>();

			//// Many-to-many
			this.Suppliers = new List<SupplierVM>();
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
		public IList<ProductDetailVM> Details { get; set; }

		//// Many-to-many

		/// <summary>
        /// The related Suppliers of Product.
        /// </summary>
		public IList<SupplierVM> Suppliers { get; set; }

        ////// To create a link with Supplier directly on create of Product.
        public Guid? SupplierId { get; set; }
        public string SupplierComment { get; set; }
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
		public User CreatedByUser { get; set; }

		/// <summary>
        /// The Id of the user who last modified the record
        /// </summary>
		public Guid ModifiedByUserId { get; set; }

		/// <summary>
        /// The user who last modified the record
        /// </summary>
		public User ModifiedByUser { get; set; }

    }
}
