using CodeGen.Extensions;
using System.Collections.Generic;

namespace CodeGen.Models
{
    public class CodeGenModel
    {
        public CodeGenModel()
        {
            Properties = new List<CodeGenModelProperty>();
            Relations = new CodeGenModelRelations();
        }

        public string Name { get; set; }
        private string _namePlural;
        public string NamePlural {
            get {
                return string.IsNullOrEmpty(_namePlural) ? Name.ToPlural() : _namePlural;
            }
            set {
                _namePlural = value;
            }
        }
        private string _displayName;
        public string DisplayName {
            get {
                return string.IsNullOrEmpty(_displayName) ? Name : _displayName;
            }
            set {
                _displayName = value;
            }
        }
        public bool Audit { get; set; }
        public bool ManyToMany { get; set; }

        public List<CodeGenModelProperty> Properties { get; set; }
        public CodeGenModelRelations Relations { get; set; }
    }
}
