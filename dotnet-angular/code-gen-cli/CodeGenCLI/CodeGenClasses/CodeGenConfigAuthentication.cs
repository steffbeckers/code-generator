using Newtonsoft.Json;

namespace CodeGenCLI.CodeGenClasses
{
    public class CodeGenConfigAuthentication
    {
        [JsonRequired]
        public bool Enabled { get; set; }
        public string Secret { get; set; }
        public string TokenExpiresInMinutes { get; set; }
        public CodeGenConfigAuthenticationAdmin Admin { get; set; }
        public string PasswordResetURL { get; set; }
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
