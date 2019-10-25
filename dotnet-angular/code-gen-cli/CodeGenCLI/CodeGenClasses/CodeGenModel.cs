using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodeGenCLI.CodeGenClasses
{

    [JsonObject("model")]
    public class CodeGenModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("namePlural")]
        public string NamePlural { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public List<CodeGenModelProperty> Properties { get; set; }
    }
}
