namespace CodeGen.Models
{
    public class CodeGenModelProperty
    {
        public string Name { get; set; }
        private string _displayName;
        public string DisplayName {
            get {
                return string.IsNullOrEmpty(_displayName) ? Name : _displayName;
            }
            set {
                _displayName = value;
            }
        }
        public string Type { get; set; }
        public bool Key { get; set; }
        public bool Required { get; set; }
        public int? MaxLength { get; set; }
        public string DefaultValue { get; set; }
        public string ColumnType { get; set; }
        public bool AuditCreatedBy { get; set; }
        public bool AuditDateCreated { get; set; }
        public bool AuditModifiedBy { get; set; }
        public bool AuditDateModified { get; set; }
        public bool AuditDeleted { get; set; }
  }
}
