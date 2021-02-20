using CodeGen.API.Validation;
using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeGen.API.Models
{
    public class Project : Auditable
    {
        public Project()
        {
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TemplateName { get; set; }


    }

    public class ProjectValidator : AbstractValidator<Project>, IValidatorInitilizer
    {
        public ProjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage(string.Format("Name has a {0} character limit", 100))
            ;
            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage(string.Format("Description has a {0} character limit", 100))
            ;
            RuleFor(x => x.TemplateName)
                .NotEmpty().WithMessage("TemplateName is required")
                .MaximumLength(100).WithMessage(string.Format("TemplateName has a {0} character limit", 100))
            ;
        }

        public void Init()
        {
        }
    }
}
