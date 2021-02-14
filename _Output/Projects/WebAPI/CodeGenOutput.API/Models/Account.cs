using FluentValidation;
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

    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage(string.Format("Name has a {0} character limit", 100))
            ;
            RuleFor(x => x.Description)
                .MaximumLength(4000).WithMessage(string.Format("Description has a {0} character limit", 4000))
            ;
            RuleFor(x => x.Telephone)
                .MaximumLength(100).WithMessage(string.Format("Telephone has a {0} character limit", 100))
            ;
            RuleFor(x => x.Email)
                .MaximumLength(100).WithMessage(string.Format("Email has a {0} character limit", 100))
            ;
            RuleFor(x => x.Website)
                .MaximumLength(100).WithMessage(string.Format("Website has a {0} character limit", 100))
            ;
            RuleFor(x => x.VAT)
                .MaximumLength(100).WithMessage(string.Format("VAT has a {0} character limit", 100))
            ;
        }
    }
}
