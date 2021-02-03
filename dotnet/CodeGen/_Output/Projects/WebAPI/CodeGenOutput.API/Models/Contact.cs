using System;
using System.Collections.Generic;

namespace CodeGenOutput.Models
{
	public class Contact
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Telephone { get; set; }
		public string Email { get; set; }
	}
}