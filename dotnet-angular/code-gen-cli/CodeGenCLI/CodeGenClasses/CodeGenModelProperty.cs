using Newtonsoft.Json;

namespace CodeGenCLI.CodeGenClasses
{
    [JsonObject("property")]
    public class CodeGenModelProperty
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
