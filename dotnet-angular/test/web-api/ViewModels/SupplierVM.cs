using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Supplier view model
    /// </summary>
    public class SupplierVM
    {
		public SupplierVM()
        {
            // Relations

			//// Many-to-many
			this.Products = new List<ProductVM>();
        }

		// Properties

		/// <summary>
        /// The identifier of Supplier.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Supplier.
        /// </summary>
        [Required]
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
		public IList<ProductVM> Products { get; set; }

        ////// To create a link with Product directly on create of Supplier.
        public Guid? ProductId { get; set; }
        public string ProductComment { get; set; }

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
