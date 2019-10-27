using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodeGenCLI.CodeGenClasses
{
    [JsonObject("config")]
    public class CodeGenConfig
    {
        public CodeGenConfig()
        {
            this.Models = new List<CodeGenModel>();
        }

        [JsonRequired]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("override")]
        public bool Override { get; set; }

        [JsonProperty("webAPI")]
        public CodeGenConfigWebAPI WebAPI { get; set; }

        public IList<CodeGenModel> Models { get; set; }
    }
}
