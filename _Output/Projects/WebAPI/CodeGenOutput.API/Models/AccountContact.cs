using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodeGenOutput.API.Validation;

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

    public class AccountContactValidator : AbstractValidator<AccountContact>
    {
        public AccountContactValidator()
        {
            RuleFor(x => x.IsPrimary)
            ;
            RuleFor(x => x.SortOrder)
            ;
        }

        public void Init()
        {
            RuleFor(x => x.Account)
                .SetValidator(Validators.AccountValidator);
            RuleFor(x => x.Contact)
                .SetValidator(Validators.ContactValidator);
        }
    }
}
