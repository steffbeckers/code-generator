using System;
using System.Collections.Generic;

namespace Test.API.Models
{
	/// <summary>
    /// Address model
    /// </summary>
    public class Address
    {
		public Address()
        {
            // Relations
        }

		// Properties

		/// <summary>
        /// The identifier of Address.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Street property of Address.
        /// </summary>
		public string Street { get; set; }

		/// <summary>
        /// The Number property of Address.
        /// </summary>
		public string Number { get; set; }

		/// <summary>
        /// The PostalCode property of Address.
        /// </summary>
		public string PostalCode { get; set; }

		/// <summary>
        /// The City property of Address.
        /// </summary>
		public string City { get; set; }

		/// <summary>
        /// The Primary property of Address.
        /// </summary>
		public bool Primary { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key AccountId for Account of Address.
        /// </summary>
		public Guid? AccountId { get; set; }

		/// <summary>
        /// The related Account of Address.
        /// </summary>
		public Account Account { get; set; }


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
