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
        /// The City property of Address.
        /// </summary>
		public string City { get; set; }

		/// <summary>
        /// The State property of Address.
        /// </summary>
		public string State { get; set; }

		/// <summary>
        /// The PostalCode property of Address.
        /// </summary>
		public string PostalCode { get; set; }

		/// <summary>
        /// The Latitude property of Address.
        /// </summary>
		public decimal Latitude { get; set; }

		/// <summary>
        /// The Longitude property of Address.
        /// </summary>
		public decimal Longitude { get; set; }

		/// <summary>
        /// The CountryId property of Address.
        /// </summary>
		public Guid? CountryId { get; set; }


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

		/// <summary>
        /// The Id of the user who created the record
        /// </summary>
		public Guid CreatedByUserId { get; set; }

		/// <summary>
        /// The user who created the record
        /// </summary>
		public User CreatedByUser { get; set; }

		/// <summary>
        /// The Id of the user who last modified the record
        /// </summary>
		public Guid ModifiedByUserId { get; set; }

		/// <summary>
        /// The user who last modified the record
        /// </summary>
		public User ModifiedByUser { get; set; }

		// TODO: Multi-tenancy
		//public Guid TenantId { get; set; }
    }
}
