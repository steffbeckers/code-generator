using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodeGenCLI.CodeGenClasses
{
    [JsonObject("webAPI")]
    public class CodeGenWebAPIConfig
    {
        [JsonProperty("projectPath")]
        public string ProjectPath { get; set; }

        [JsonProperty("namespaceRoot")]
        public string NamespaceRoot { get; set; }

        [JsonProperty("modelsPath")]
        public string ModelsPath { get; set; }

        [JsonProperty("viewModelsPath")]
        public string ViewModelsPath { get; set; }

        [JsonProperty("dalPath")]
        public string DALPath { get; set; }
    }
}
