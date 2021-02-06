using System.Collections.Generic;

namespace CodeGen.Models
{
    public class CodeGenConfig
    {
        public CodeGenConfig()
        {
            Models = new List<CodeGenModel>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<CodeGenModel> Models { get; set; }
        public CodeGenConfigPaths Paths { get; set; }
        public CodeGenConfigProjects Projects { get; set; }
    }
}
