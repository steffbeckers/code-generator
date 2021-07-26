using CodeGenOutput.API.Validation;
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
            this.Contacts = new List<Contact>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }


        public ICollection<Contact> Contacts { get; set; }
    }

    public class AccountValidator : AbstractValidator<Account>, IValidatorInitilizer
    {
        public AccountValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage(string.Format("Name has a {0} character limit", 100))
            ;
            RuleFor(x => x.Description)
                .MaximumLength(512).WithMessage(string.Format("Description has a {0} character limit", 512))
            ;
            RuleFor(x => x.Telephone)
                .MaximumLength(100).WithMessage(string.Format("Telephone has a {0} character limit", 100))
            ;
            RuleFor(x => x.Email)
                .MaximumLength(100).WithMessage(string.Format("E-mail has a {0} character limit", 100))
            ;
            RuleFor(x => x.Website)
                .MaximumLength(100).WithMessage(string.Format("Website has a {0} character limit", 100))
            ;
        }

        public void Init()
        {
            RuleForEach(x => x.Contacts)
                .SetValidator(Validators.ContactValidator);
        }
    }
}
