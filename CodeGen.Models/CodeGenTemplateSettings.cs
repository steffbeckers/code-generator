using System.Collections.Generic;

namespace CodeGen.Models
{
    public class CodeGenTemplateSettings
    {
        public CodeGenTemplateSettings()
        {
            ConfigBasedGenerator = new List<CodeGenTemplateSettingsData>();
            ModelsBasedGenerator = new List<CodeGenTemplateSettingsData>();
            Exclude = new List<string>();
        }

        public string TemplatePath { get; set; }
        public CodeGenTemplateSettingsAngular Angular { get; set; }
        public CodeGenTemplateSettingsDotNET DotNET { get; set; }

        public List<CodeGenTemplateSettingsData> ConfigBasedGenerator { get; set; }
        public List<CodeGenTemplateSettingsData> ModelsBasedGenerator { get; set; }
        public List<string> Exclude { get; set; }
    }
}
