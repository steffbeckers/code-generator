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
        }

        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public Guid? AccountId { get; set; }
        public Account Account { get; set; }

    }
}
