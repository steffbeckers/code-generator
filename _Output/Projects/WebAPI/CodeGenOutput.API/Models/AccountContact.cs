using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeGenOutput.API.Models
{
    public class AccountContact : Auditable
    {
        public AccountContact()
        {
        }

        [Key]
        public Guid Id { get; set; }
        public bool IsPrimary { get; set; }
        public int? SortOrder { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }

    }
}
