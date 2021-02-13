using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeGenOutput.API.Models
{
    public class Account : Auditable
    {
        public Account()
        {
            this.Contacts = new List<AccountContact>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string VAT { get; set; }

        public Guid? AddressId { get; set; }
        public Address Address { get; set; }

        public ICollection<AccountContact> Contacts { get; set; }
    }
}
