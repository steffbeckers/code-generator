using System.Collections.Generic;
using System.Linq;

namespace CodeGen.Models
{
    public class CodeGenConfig
    {
        public CodeGenModels Models { get; set; }
        public CodeGenConfigPaths Paths { get; set; }
    }
}
