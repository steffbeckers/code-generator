namespace CodeGen.Models
{
    public class CodeGenTemplateSettingsAfterGenerate
    {
        public bool Build { get; set; }
        public bool RecreateDatabase { get; set; }
        public bool InstallProjectTemplate { get; set; }
        public bool OpenSwagger { get; set; }
        public bool Run { get; set; }
  }
}
