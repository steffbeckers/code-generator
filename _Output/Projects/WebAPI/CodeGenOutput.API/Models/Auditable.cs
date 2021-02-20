using System;

namespace CodeGenOutput.API.Models
{
    public abstract class Auditable
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Deleted { get; set; }
    }
}
