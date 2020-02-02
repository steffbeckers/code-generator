using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
	/// <summary>
    /// Account view model
    /// </summary>
    public class AccountVM
    {
		public AccountVM()
        {
            // Relations

			//// One-to-many
			this.ChildAccounts = new List<AccountVM>();
        }

		// Properties

		/// <summary>
        /// The identifier of Account.
        /// </summary>
		public Guid Id { get; set; }

		/// <summary>
        /// The Name property of Account.
        /// </summary>
        [Required]
		public string Name { get; set; }

		/// <summary>
        /// The Website property of Account.
        /// </summary>
		public string Website { get; set; }

		/// <summary>
        /// The Telephone property of Account.
        /// </summary>
		public string Telephone { get; set; }

		/// <summary>
        /// The Email property of Account.
        /// </summary>
		public string Email { get; set; }

		// Relations

		//// Many-to-one

	    /// <summary>
        /// The related foreign key ParentAccountId for ParentAccount of Account.
        /// </summary>
		public Guid? AccountId { get; set; }

		/// <summary>
        /// The related ParentAccount of Account.
        /// </summary>
		public AccountVM ParentAccount { get; set; }


		//// One-to-many

		/// <summary>
        /// The related ChildAccounts of Account.
        /// </summary>
		public IList<AccountVM> ChildAccounts { get; set; }
    }
}
