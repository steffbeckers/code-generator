using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodeGenCLI.CodeGenClasses
{
    [JsonObject("config")]
    public class CodeGenConfig
    {
        [JsonRequired]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("override")]
        public bool Override { get; set; }

        [JsonProperty("webAPI")]
        public CodeGenWebAPIConfig WebAPI { get; set; }

        public List<CodeGenModel> Models { get; set; }
    }
}
