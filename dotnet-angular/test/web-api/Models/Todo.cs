using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// Todo model
    /// </summary>
    public class Todo
    {
		public Todo()
        {
            // Relations
        }

		// Properties

		/// <summary>
        /// The identifier of Todo.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Title property of Todo.
        /// </summary>
		public string Title { get; set; }

		/// <summary>
        /// The Body property of Todo.
        /// </summary>
		public string Body { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key ProjectId for Project of Todo.
        /// </summary>
		public Guid? ProjectId { get; set; }

		/// <summary>
        /// The related Project of Todo.
        /// </summary>
		public Project Project { get; set; }


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
