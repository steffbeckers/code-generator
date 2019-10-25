using System;

namespace CRM.Models
{
    public class Note
    {
		public string Title { get; set; }
		
		// Generic 
		// TODO: To base class?
		public Guid Id { get; set; }
    }
}
