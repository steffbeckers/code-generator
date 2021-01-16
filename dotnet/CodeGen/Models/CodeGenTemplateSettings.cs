using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.Models
{
    public class CodeGenTemplateSettings
    {
        public CodeGenTemplateSettings()
        {
            GenerateForEachModel = new List<GenerateForEachModelData>();
        }

        public List<GenerateForEachModelData> GenerateForEachModel { get; set; }
    }

    public class GenerateForEachModelData
    {
        public string T4Template { get; set; }
        public string Output { get; set; }
    }
}
