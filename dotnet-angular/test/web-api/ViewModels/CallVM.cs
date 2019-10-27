using System;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
    public class CallVM
    {
		public Guid Id { get; set; }

        [Required]
		public DateTime Date { get; set; }
    }
}
