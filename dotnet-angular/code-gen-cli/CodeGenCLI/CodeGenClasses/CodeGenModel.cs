using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodeGenCLI.CodeGenClasses
{
    public class CodeGenModel
    {
        public CodeGenModel()
        {
            this.Properties = new List<CodeGenModelProperty>();
            this.Relations = new List<CodeGenModelRelation>();
        }

        [JsonRequired]
        public string Name { get; set; }
        public string NamePlural { get; set; }
        public string Description { get; set; }
        public string DisplayField { get; set; }
        public string SortField { get; set; }
        public string DatabaseTableName { get; set; }
        public string DatabaseKey { get; set; }
        public bool ManyToMany { get; set; }

        public IList<CodeGenModelProperty> Properties { get; set; }
        public IList<CodeGenModelRelation> Relations { get; set; }
    }

    public class CodeGenModelProperty
    {
        [JsonRequired]
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        [JsonRequired]
        public string Type { get; set; }

        [JsonRequired]
        public bool Required { get; set; }

        public string DatabaseFieldName { get; set; }
    }

    public class CodeGenModelRelation
    {
        [JsonRequired]
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        [JsonRequired]
        public string Type { get; set; }

        [JsonRequired]
        public bool Required { get; set; }

        [JsonRequired]
        public string Model { get; set; }
        public string Through { get; set; }
    }
}
