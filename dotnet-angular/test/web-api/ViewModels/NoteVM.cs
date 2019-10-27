using System;
using System.ComponentModel.DataAnnotations;

namespace Test.API.ViewModels
{
    public class NoteVM
    {
		public Guid Id { get; set; }

        [Required]
		public string Title { get; set; }
		public string Body { get; set; }
    }
}
