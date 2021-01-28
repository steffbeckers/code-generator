using System;
using System.Collections.Generic;

namespace CodeGenOutput.ViewModels
{
	public class ContactVM
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}
}
