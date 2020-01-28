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
    }
}
