using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodeGenCLI.CodeGenClasses
{
    public class CodeGenConfig
    {
        public CodeGenConfig()
        {
            this.Models = new List<CodeGenModel>();
        }

        [JsonRequired]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Override { get; set; }
        public CodeGenConfigAuthentication Authentication { get; set; }
        public CodeGenConfigWebAPI WebAPI { get; set; }
        public CodeGenConfigAngular Angular { get; set; }
        public IList<CodeGenModel> Models { get; set; }
    }
}
