﻿using Newtonsoft.Json;

namespace CodeGenCLI.CodeGenClasses
{
    [JsonObject("angular")]
    public class CodeGenConfigAngular
    {
        [JsonProperty("projectPath")]
        public string ProjectPath { get; set; }

        [JsonProperty("modelsPath")]
        public string ModelsPath { get; set; }

        [JsonProperty("modelsPathForTypeScript")]
        public string ModelsPathForTypeScript { get; set; }

        [JsonProperty("servicesPath")]
        public string ServicesPath { get; set; }
        
        [JsonProperty("servicesPathForTypeScript")]
        public string ServicesPathForTypeScript { get; set; }
    }
}
