using System;

namespace CRM.Models
{
    public class Account
    {
		public string Name { get; set; }
		
		// Generic 
		// TODO: To base class?
		public Guid Id { get; set; }
    }
}
