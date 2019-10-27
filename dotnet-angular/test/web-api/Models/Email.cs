using System;

namespace Test.API.Models
{
    public class Email
    {
		public string Subject { get; set; }
		public string Body { get; set; }

		// Generic
		public Guid Id { get; set; }
		//public Guid TenantId { get; set; }
		public DateTime CreatedOn { get; set; }
		//public Guid CreatedByUserId { get; set; }
		public DateTime ModifiedOn { get; set; }
		//public Guid ModifiedByUserId { get; set; }
		public DateTime? DeletedOn { get; set; }
    }
}
