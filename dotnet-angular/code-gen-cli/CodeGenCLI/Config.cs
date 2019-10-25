using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenCLI
{
    [JsonObject("config")]
    public class Config
    {
        [JsonRequired]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("override")]
        public bool Override { get; set; }

        public List<Model> Models { get; set; }
    }

    public class Model
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("namePlural")]
        public string NamePlural { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
