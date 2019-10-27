using System;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
    public class DocumentVM
    {
		public Guid Id { get; set; }

        [Required]
		public string Name { get; set; }
    }
}
