using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// Call model
    /// </summary>
    public class Call
    {
		public Call()
        {
        }

		// Properties

		/// <summary>
        /// The identifier of Call.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Date property of Call.
        /// </summary>
		public DateTime Date { get; set; }


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
