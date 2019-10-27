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
            this.Contacts = new List<ContactVM>();
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

        //// One-to-many

        /// <summary>
        /// The related Contacts of Account.
        /// </summary>
        public IList<ContactVM> Contacts { get; set; }
    }
}
