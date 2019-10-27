using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
    /// <summary>
    /// Project view model
    /// </summary>
    public class ProjectVM
    {
        public ProjectVM()
        {
            // Relations

            //// One-to-many
            this.Todoes = new List<TodoVM>();

            //// Many-to-many
            this.Notes = new List<NoteVM>();
        }

        // Properties

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

        // Relations

        //// One-to-many

        /// <summary>
        /// The related Todoes of Project.
        /// </summary>
        public IList<TodoVM> Todoes { get; set; }

        //// Many-to-many

        /// <summary>
        /// The related Notes of Project.
        /// </summary>
        public IList<NoteVM> Notes { get; set; }
    }
}
