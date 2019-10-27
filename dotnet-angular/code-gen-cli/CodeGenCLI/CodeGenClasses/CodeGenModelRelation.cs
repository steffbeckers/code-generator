using Newtonsoft.Json;

namespace CodeGenCLI.CodeGenClasses
{
    [JsonObject("relation")]
    public class CodeGenModelRelation
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("idField")]
        public string IdField { get; set; }

        [JsonProperty("through")]
        public string Through { get; set; }
    }
}
