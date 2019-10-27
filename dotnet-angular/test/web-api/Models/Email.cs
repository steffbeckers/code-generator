using System;

namespace Test.API.Models
{
    /// <summary>
    /// Email model
    /// </summary>
    public class Email
    {
        public Email()
        {
        }

        // Properties

        /// <summary>
        /// The identifier of Email.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The Subject property of Email.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The Body property of Email.
        /// </summary>
        public string Body { get; set; }


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
