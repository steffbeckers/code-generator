﻿using Newtonsoft.Json;

namespace CodeGenCLI.CodeGenClasses
{
    [JsonObject("webAPI")]
    public class CodeGenConfigWebAPI
    {
        [JsonProperty("databaseConnection")]
        public string DatabaseConnection { get; set; }

        [JsonProperty("projectPath")]
        public string ProjectPath { get; set; }

        [JsonProperty("namespaceRoot")]
        public string NamespaceRoot { get; set; }

        [JsonProperty("modelsPath")]
        public string ModelsPath { get; set; }

        [JsonProperty("viewModelsPath")]
        public string ViewModelsPath { get; set; }

        [JsonProperty("dalPath")]
        public string DALPath { get; set; }

        [JsonProperty("bllPath")]
        public string BLLPath { get; set; }

        [JsonProperty("controllersPath")]
        public string ControllersPath { get; set; }
    }
}
