using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodeGenCLI.CodeGenClasses
{
    public class CodeGenConfigAuthentication
    {
        public CodeGenConfigAuthentication()
        {
            this.OtherRoles = new List<string>();
        }

        [JsonRequired]
        public bool Enabled { get; set; }
        public string Secret { get; set; }
        public string TokenExpiresInMinutes { get; set; }
        [JsonRequired]
        public bool EmailConfirmation { get; set; }
        public string ConfirmEmailURL { get; set; }
        public string ResetPasswordURL { get; set; }
        public CodeGenConfigAuthenticationAdmin Admin { get; set; }
        public IList<string> OtherRoles { get; set; }
    }

    public class CodeGenConfigAuthenticationAdmin
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonRequired]
        public string Username { get; set; }

        [JsonRequired]
        public string Email { get; set; }

        [JsonRequired]
        public string Password { get; set; }
    }
}
