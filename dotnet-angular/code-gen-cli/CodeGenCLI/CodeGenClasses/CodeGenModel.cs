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

        [JsonProperty("displayField")]
        public string DisplayField { get; set; }

        [JsonProperty("sortField")]
        public string SortField { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("manyToMany")]
        public bool ManyToMany { get; set; }

        [JsonProperty("properties")]
        public IList<CodeGenModelProperty> Properties { get; set; }
        [JsonProperty("relations")]
        public IList<CodeGenModelRelation> Relations { get; set; }
    }
}
