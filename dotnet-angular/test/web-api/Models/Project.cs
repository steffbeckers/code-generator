using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// Project model
    /// </summary>
    public class Project
    {
		public Project()
        {
            // Relations

			//// One-to-many
			this.Todoes = new List<Todo>();

			//// Many-to-many
			this.ProjectNote = new List<ProjectNote>();
        }

		// Properties

		/// <summary>
        /// The identifier of Project.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Project.
        /// </summary>
		public string Name { get; set; }

		/// <summary>
        /// The Description property of Project.
        /// </summary>
		public string Description { get; set; }

		// Relations

		//// One-to-many

		/// <summary>
        /// The related Todoes of Project.
        /// </summary>
		public IList<Todo> Todoes { get; set; }

		//// Many-to-many

		/// <summary>
        /// The related Notes of Project.
        /// </summary>
		public IList<ProjectNote> ProjectNote { get; set; }

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
