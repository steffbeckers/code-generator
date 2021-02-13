using CodeGen.Extensions;
using System.Collections.Generic;

namespace CodeGen.Models
{
    public class CodeGenModel
    {
        public CodeGenModel()
        {
            Properties = new List<CodeGenModelProperty>();
        }

        public string Name { get; set; }
        public string NamePlural {
            get {
                return Name.ToPlural();
            }
        }
        public bool Audit { get; set; }
        public string GenericSearchTermFilter { get; set; } = "x => x.Name.Contains(term)";

        public List<CodeGenModelProperty> Properties { get; set; }
        public CodeGenModelRelations Relations { get; set; }
    }
}
