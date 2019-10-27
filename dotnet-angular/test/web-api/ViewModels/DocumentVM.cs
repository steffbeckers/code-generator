using System;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Document view model
    /// </summary>
    public class DocumentVM
    {
		/// <summary>
        /// The identifier of Document.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Document.
        /// </summary>
        [Required]
		public string Name { get; set; }
    }
}
