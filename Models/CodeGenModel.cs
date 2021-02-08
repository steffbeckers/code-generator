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
        public string Description { get; set; }
        public bool Audit { get; set; }

        public List<CodeGenModelProperty> Properties { get; set; }
    }
}
