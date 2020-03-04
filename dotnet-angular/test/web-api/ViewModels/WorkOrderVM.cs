using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.API.ViewModels.Identity;

namespace Test.API.ViewModels
{
	/// <summary>
    /// WorkOrder view model
    /// </summary>
    public class WorkOrderVM
    {
		public WorkOrderVM()
        {
        }

		// Properties

		/// <summary>
        /// The identifier of WorkOrder.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Id property of WorkOrder.
        /// </summary>
        [Required]
		public Guid Id { get; set; }

		/// <summary>
        /// The Date property of WorkOrder.
        /// </summary>
        [Required]
		public DateTime Date { get; set; }

		/// <summary>
        /// The AccountId property of WorkOrder.
        /// </summary>
        [Required]
		public Guid AccountId { get; set; }


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
