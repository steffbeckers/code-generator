using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.API.ViewModels.Identity;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Cart view model
    /// </summary>
    public class CartVM
    {
		public CartVM()
        {
            // Relations

			//// Many-to-many
			this.Products = new List<ProductVM>();
        }

		// Properties

		/// <summary>
        /// The identifier of Cart.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Cart.
        /// </summary>
        [Required]
		public string Name { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key UserId for User of Cart.
        /// </summary>
		public Guid? UserId { get; set; }

		/// <summary>
        /// The related User of Cart.
        /// </summary>
		public UserVM User { get; set; }


		//// Many-to-many

		/// <summary>
        /// The related Products of Cart.
        /// </summary>
		public IList<ProductVM> Products { get; set; }

        ////// To create a link with Product directly on create of Cart.
        public Guid? ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductPrice { get; set; }

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
