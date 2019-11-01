using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Address view model
    /// </summary>
    public class AddressVM
    {
		public AddressVM()
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
		public AccountVM Account { get; set; }

    }
}
