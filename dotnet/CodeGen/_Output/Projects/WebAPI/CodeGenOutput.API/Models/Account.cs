using System;
using System.Collections.Generic;

namespace CodeGenOutput.Models
{
	public class Account
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Telephone { get; set; }
		public string Email { get; set; }
		public string Website { get; set; }
		public string VAT { get; set; }
	}
}
