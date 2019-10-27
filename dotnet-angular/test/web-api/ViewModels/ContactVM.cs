using System;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
    public class ContactVM
    {
		public Guid Id { get; set; }

        [Required]
		public string FirstName { get; set; }

        [Required]
		public string LastName { get; set; }
    }
}
