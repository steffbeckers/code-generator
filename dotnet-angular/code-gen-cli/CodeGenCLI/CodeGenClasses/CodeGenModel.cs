using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodeGenCLI.CodeGenClasses
{
    [JsonObject("model")]
    public class CodeGenModel
    {
        public CodeGenModel()
        {
            this.Properties = new List<CodeGenModelProperty>();
            this.Relations = new List<CodeGenModelRelation>();
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("namePlural")]
        public string NamePlural { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public IList<CodeGenModelProperty> Properties { get; set; }
        public IList<CodeGenModelRelation> Relations { get; set; }
    }
}
