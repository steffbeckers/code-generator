using System;

namespace Test.API.Models
{
    public class Account
    {
		public string Name { get; set; }
		public string Website { get; set; }
		public string Telephone { get; set; }
		public string Email { get; set; }

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
