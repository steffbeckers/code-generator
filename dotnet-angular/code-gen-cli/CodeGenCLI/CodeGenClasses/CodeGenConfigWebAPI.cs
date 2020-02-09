using Newtonsoft.Json;

namespace CodeGenCLI.CodeGenClasses
{
    public class CodeGenConfigWebAPI
    {
        [JsonRequired]
        public string DatabaseConnection { get; set; }
        public string NamespaceRoot { get; set; }
        public CodeGenConfigWebAPIEmailService EmailService { get; set; }

        // Paths
        [JsonRequired]
        public string ProjectPath { get; set; }
        public string ModelsPath { get; set; }
        public string ViewModelsPath { get; set; }
        public string DALPath { get; set; }
        public string BLLPath { get; set; }
        public string ControllersPath { get; set; }
        public string ServicesPath { get; set; }
        public string GraphQLPath { get; set; }
    }

    public class CodeGenConfigWebAPIEmailService
    {
        [JsonRequired]
        public bool Enabled { get; set; }

        [JsonRequired]
        public string MailServer { get; set; }

        [JsonRequired]
        public int MailPort { get; set; }

        [JsonRequired]
        public bool UseSSL { get; set; }

        [JsonRequired]
        public string SenderName { get; set; }

        [JsonRequired]
        public string Sender { get; set; }

        [JsonRequired]
        public string Password { get; set; }
    }
}
