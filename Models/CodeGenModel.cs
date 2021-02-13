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
        public bool Audit { get; set; }
        public bool ManyToMany { get; set; }
        public string GenericSearchTermFilter { get; set; } = "x => x.Name.Contains(term)";

        public List<CodeGenModelProperty> Properties { get; set; }
        public CodeGenModelRelations Relations { get; set; }
    }
}
