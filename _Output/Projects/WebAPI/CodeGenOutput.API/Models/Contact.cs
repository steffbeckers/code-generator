using CodeGenOutput.API.Validation;
using FluentValidation;
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

    public class ContactValidator : AbstractValidator<Contact>, IValidatorInitilizer
    {
        public ContactValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100).WithMessage(string.Format("First name has a {0} character limit", 100))
            ;
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage(string.Format("Last name has a {0} character limit", 100))
            ;
            RuleFor(x => x.Telephone)
                .MaximumLength(100).WithMessage(string.Format("Telephone has a {0} character limit", 100))
            ;
            RuleFor(x => x.Email)
                .MaximumLength(100).WithMessage(string.Format("Email has a {0} character limit", 100))
            ;
        }

        public void Init()
        {
            RuleFor(x => x.Address)
                .SetValidator(Validators.AddressValidator);
            RuleForEach(x => x.Account)
                .SetValidator(Validators.AccountContactValidator);
        }
    }
}
