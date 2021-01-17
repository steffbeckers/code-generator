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
            ConfigBasedGenerator = new List<CodeGenTemplateSettingsData>();
            ModelsBasedGenerator = new List<CodeGenTemplateSettingsData>();
        }

        public List<CodeGenTemplateSettingsData> ConfigBasedGenerator { get; set; }
        public List<CodeGenTemplateSettingsData> ModelsBasedGenerator { get; set; }
    }

    public class CodeGenTemplateSettingsData
    {
        public string T4Template { get; set; }
        public string Output { get; set; }
    }
}
