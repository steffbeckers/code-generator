using Newtonsoft.Json;

namespace CodeGenCLI.CodeGenClasses
{
    public class CodeGenConfigAngular
    {
        [JsonRequired]
        public string ProjectPath { get; set; }
        public string ModelsPath { get; set; }
        public string ModelsPathForTypeScript { get; set; }
        public string ServicesPath { get; set; }
        public string ServicesPathForTypeScript { get; set; }
    }
}
