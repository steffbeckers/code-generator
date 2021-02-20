using System.Collections.Generic;

namespace CodeGen.Models
{
    public class CodeGenTemplateSettings
    {
        public CodeGenTemplateSettings()
        {
            ConfigBasedGenerator = new List<CodeGenTemplateSettingsData>();
            ModelsBasedGenerator = new List<CodeGenTemplateSettingsData>();
        }

        public bool Generate { get; set; }
        public CodeGenTemplateSettingsBeforeGenerate BeforeGenerate { get; set; }
        public CodeGenTemplateSettingsAfterGenerate AfterGenerate { get; set; }

        public string StartupProjectPath { get; set; }
        public string StartupProjectURL { get; set; }
        public string MigrationsFolderPath { get; set; }
        // Filled by generator
        public string TemplatePath { get; set; }

        public List<CodeGenTemplateSettingsData> ConfigBasedGenerator { get; set; }
        public List<CodeGenTemplateSettingsData> ModelsBasedGenerator { get; set; }
    }
}
