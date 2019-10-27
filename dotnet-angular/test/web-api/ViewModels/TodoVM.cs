using System;

namespace Test.API.ViewModels
{
    /// <summary>
    /// Todo view model
    /// </summary>
    public class TodoVM
    {
        public TodoVM()
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
        public ProjectVM Project { get; set; }

    }
}
