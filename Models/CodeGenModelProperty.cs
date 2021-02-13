namespace CodeGen.Models
{
    public class CodeGenModelProperty
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        // TODO: Required fields
        //public bool Required { get; set; }
        public bool Key { get; set; }
        public bool AuditCreatedBy { get; set; }
        public bool AuditDateCreated { get; set; }
        public bool AuditModifiedBy { get; set; }
        public bool AuditDateModified { get; set; }
        public bool AuditDeleted { get; set; }
  }
}
