using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeGenOutput.API.Models
{
    public class Contact : Auditable
    {
        public Contact()
        {
            this.Account = new List<AccountContact>();
        }

        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public Guid? AddressId { get; set; }
        public Address Address { get; set; }

        public ICollection<AccountContact> Account { get; set; }
    }
}
