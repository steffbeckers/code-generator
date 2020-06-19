using System;
using System.Collections.Generic;

namespace RJM.API.Models
{
	/// <summary>
    /// Setting model
    /// </summary>
    public class Setting
    {
		public Setting()
        {
        }

		// Properties

		/// <summary>
        /// The identifier of Setting.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Key property of Setting.
        /// </summary>
		public string Key { get; set; }

		/// <summary>
        /// The Value property of Setting.
        /// </summary>
		public string Value { get; set; }


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

		// TODO: Multi-tenancy
		//public Guid TenantId { get; set; }
    }
}
