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

        public bool Generate { get; set; }
        public bool CommitProjectOutputDirectoryBeforeGenerate { get; set; }
        public bool RecreateDatabaseAfterGenerate { get; set; }
        public bool InstallProjectTemplateAfterGenerate { get; set; }
        public bool TestProjectAfterGenerate { get; set; }

        public string StartupProjectPath { get; set; }
        public string MigrationsFolderPath { get; set; }
        // Filled by generator
        public string TemplatePath { get; set; }

        public List<CodeGenTemplateSettingsData> ConfigBasedGenerator { get; set; }
        public List<CodeGenTemplateSettingsData> ModelsBasedGenerator { get; set; }
  }

    public class CodeGenTemplateSettingsData
    {
        public string T4Template { get; set; }
        public string Output { get; set; }
    }
}
