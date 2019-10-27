using System;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Project view model
    /// </summary>
    public class ProjectVM
    {
		/// <summary>
        /// The identifier of Project.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Project.
        /// </summary>
        [Required]
		public string Name { get; set; }

		/// <summary>
        /// The Description property of Project.
        /// </summary>
		public string Description { get; set; }
    }
}
