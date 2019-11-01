using Newtonsoft.Json;

namespace CodeGenCLI.CodeGenClasses
{
    [JsonObject("angular")]
    public class CodeGenConfigAngular
    {
        [JsonProperty("projectPath")]
        public string ProjectPath { get; set; }

        [JsonProperty("modelsPath")]
        public string ModelsPath { get; set; }
    }
}
