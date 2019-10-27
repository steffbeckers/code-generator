using System;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Todo view model
    /// </summary>
    public class TodoVM
    {
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
    }
}
