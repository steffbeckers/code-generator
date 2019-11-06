using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Todo view model
    /// </summary>
    public class TodoVM
    {
		public TodoVM()
        {
        }

		// Properties

		/// <summary>
        /// The identifier of Todo.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Title property of Todo.
        /// </summary>
        [Required]
		public string Title { get; set; }

		/// <summary>
        /// The DueDate property of Todo.
        /// </summary>
		public DateTime DueDate { get; set; }

    }
}
