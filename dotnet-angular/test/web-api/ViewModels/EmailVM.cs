using System;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
    public class EmailVM
    {
		public Guid Id { get; set; }

        [Required]
		public string Subject { get; set; }
		public string Body { get; set; }
    }
}
