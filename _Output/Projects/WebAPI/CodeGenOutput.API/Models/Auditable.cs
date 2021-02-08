using System;

namespace CodeGenOutput.API.Models
{
    public abstract class Auditable
    {
        public Guid CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Deleted { get; set; }
    }
}
