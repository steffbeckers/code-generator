using CodeGen.API.Validation;
using CodeGen.Models;
using FluentValidation;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string ConfigJson { get; set; }
        [NotMapped]
        public CodeGenConfig Config
        {
            get { return string.IsNullOrEmpty(ConfigJson) ? null : JsonConvert.DeserializeObject<CodeGenConfig>(ConfigJson); }
            set {
                ConfigJson = JsonConvert.SerializeObject(value, Formatting.None,
                    new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            }
        }
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
        }

        public void Init()
        {
        }
    }
}
