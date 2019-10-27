using System;

namespace Test.API.Models
{
    /// <summary>
    /// Document model
    /// </summary>
    public class Document
    {
        public Document()
        {
        }

        // Properties

        /// <summary>
        /// The identifier of Document.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The Name property of Document.
        /// </summary>
        public string Name { get; set; }


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
