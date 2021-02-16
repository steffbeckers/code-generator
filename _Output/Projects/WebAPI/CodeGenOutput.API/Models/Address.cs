using CodeGenOutput.API.Validation;
using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeGenOutput.API.Models
{
    public class Address : Auditable
    {
        public Address()
        {
        }

        [Key]
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string BoxNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }


    }

    public class AddressValidator : AbstractValidator<Address>, IValidatorInitilizer
    {
        public AddressValidator()
        {
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required")
                .MaximumLength(100).WithMessage(string.Format("Street has a {0} character limit", 100))
            ;
            RuleFor(x => x.HouseNumber)
                .NotEmpty().WithMessage("House number is required")
                .MaximumLength(10).WithMessage(string.Format("House number has a {0} character limit", 10))
            ;
            RuleFor(x => x.BoxNumber)
                .MaximumLength(10).WithMessage(string.Format("Box number has a {0} character limit", 10))
            ;
            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("Postal code is required")
                .MaximumLength(10).WithMessage(string.Format("Postal code has a {0} character limit", 10))
            ;
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(100).WithMessage(string.Format("City has a {0} character limit", 100))
            ;
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required")
                .MaximumLength(100).WithMessage(string.Format("Country has a {0} character limit", 100))
            ;
        }

        public void Init()
        {
        }
    }
}
