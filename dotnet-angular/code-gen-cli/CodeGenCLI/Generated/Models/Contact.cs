using System;

namespace CRM.Models
{
    public class Contact
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		
		// Generic 
		// TODO: To base class?
		public Guid Id { get; set; }
    }
}
