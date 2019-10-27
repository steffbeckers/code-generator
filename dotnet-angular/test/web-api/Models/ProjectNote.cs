using System;

namespace Test.API.Models
{
    /// <summary>
    /// ProjectNote model
    /// </summary>
    public class ProjectNote
    {
        public ProjectNote()
        {
            // Relations
        }

        // Properties

        /// <summary>
        /// The identifier of ProjectNote.
        /// </summary>
        public Guid Id { get; set; }

        // Relations

        //// Many-to-one

        /// <summary>
        /// The related foreign key ProjectId for Project of ProjectNote.
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// The related Project of ProjectNote.
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// The related foreign key NoteId for Note of ProjectNote.
        /// </summary>
        public Guid NoteId { get; set; }

        /// <summary>
        /// The related Note of ProjectNote.
        /// </summary>
        public Note Note { get; set; }


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
