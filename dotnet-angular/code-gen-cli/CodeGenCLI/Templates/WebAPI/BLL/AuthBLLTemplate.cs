﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CodeGenCLI.Templates.WebAPI.BLL
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGenCLI.CodeGenClasses;
    using CodeGenCLI.Extensions;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\BLL\AuthBLLTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class AuthBLLTemplate : AuthBLLTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ");
            
            #line 21 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\BLL\AuthBLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".Framework.Exceptions;\r\nusing ");
            
            #line 22 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\BLL\AuthBLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".Models;\r\nusing ");
            
            #line 23 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\BLL\AuthBLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".Services;\r\nusing ");
            
            #line 24 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\BLL\AuthBLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".ViewModels.Identity;\r\n\r\nnamespace ");
            
            #line 26 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\BLL\AuthBLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".BLL\r\n{\r\n\t/// <summary>\r\n\t/// The business logic layer for authentication.\r\n\t/// " +
                    "</summary>\r\n    public class AuthBLL\r\n    {\r\n        private readonly IConfigura" +
                    "tion configuration;\r\n        private readonly ILogger logger;\r\n        private r" +
                    "eadonly IMapper mapper;\r\n        private readonly IHttpContextAccessor httpConte" +
                    "xtAccessor;\r\n        private readonly UserManager<User> userManager;\r\n        pr" +
                    "ivate readonly SignInManager<User> signInManager;\r\n        private readonly IEma" +
                    "ilService emailService;\r\n\r\n        public AuthBLL(\r\n            IConfiguration c" +
                    "onfiguration,\r\n            ILogger<AuthBLL> logger,\r\n            IMapper mapper," +
                    "\r\n            IHttpContextAccessor httpContextAccessor,\r\n            UserManager" +
                    "<User> userManager,\r\n            SignInManager<User> signInManager,\r\n           " +
                    " IEmailService emailService\r\n        )\r\n        {\r\n            this.configuratio" +
                    "n = configuration;\r\n            this.logger = logger;\r\n            this.mapper =" +
                    " mapper;\r\n            this.httpContextAccessor = httpContextAccessor;\r\n         " +
                    "   this.userManager = userManager;\r\n            this.signInManager = signInManag" +
                    "er;\r\n            this.emailService = emailService;\r\n        }\r\n\r\n        public " +
                    "async Task<AuthenticatedVM> Login(LoginVM loginVM)\r\n        {\r\n            // Va" +
                    "lidation\r\n            if (loginVM == null)\r\n            {\r\n                retur" +
                    "n null;\r\n            }\r\n\r\n            // Result\r\n            AuthenticatedVM aut" +
                    "henticatedVM = new AuthenticatedVM()\r\n            {\r\n                RememberMe " +
                    "= loginVM.RememberMe\r\n            };\r\n\r\n            // Retrieve user by email or" +
                    " username\r\n            User user = await userManager.FindByEmailAsync(loginVM.Em" +
                    "ailOrUsername) ?? await userManager.FindByNameAsync(loginVM.EmailOrUsername);\r\n " +
                    "           if (user == null)\r\n            {\r\n                logger.LogWarning(\"" +
                    "User not found during login\", loginVM.EmailOrUsername);\r\n\r\n                throw" +
                    " new LoginFailedException(\"invalid\");\r\n            }\r\n\r\n            // Log the u" +
                    "ser in by password\r\n            SignInResult signInResult = await signInManager." +
                    "PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, lockoutOnFailure" +
                    ": true);\r\n            \r\n            // Success\r\n            if (signInResult.Suc" +
                    "ceeded)\r\n            {\r\n                // Authenticated by password\r\n          " +
                    "      logger.LogInformation(\"User logged in\", user);\r\n\r\n                // Retri" +
                    "eve roles of user\r\n                user.Roles = (List<string>)await userManager." +
                    "GetRolesAsync(user);\r\n\r\n                // Set claims of user\r\n                L" +
                    "ist<Claim> claims = new List<Claim>() {\r\n                    new Claim(JwtRegist" +
                    "eredClaimNames.NameId, user.Id.ToString().ToUpper()),\r\n                    new C" +
                    "laim(JwtRegisteredClaimNames.UniqueName, user.UserName),\r\n                    ne" +
                    "w Claim(JwtRegisteredClaimNames.Email, user.Email),\r\n                    new Cla" +
                    "im(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.CurrentCult" +
                    "ure))\r\n                };\r\n\r\n                // TODO: Custom fields\r\n           " +
                    "     if (!string.IsNullOrEmpty(user.FirstName))\r\n                {\r\n            " +
                    "        claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName))" +
                    ";\r\n                }\r\n                if (!string.IsNullOrEmpty(user.LastName))\r" +
                    "\n                {\r\n                    claims.Add(new Claim(JwtRegisteredClaimN" +
                    "ames.FamilyName, user.LastName));\r\n                }\r\n\r\n                // Add r" +
                    "oles as claims\r\n                foreach (var role in user.Roles)\r\n              " +
                    "  {\r\n                    claims.Add(new Claim(ClaimTypes.Role, role));\r\n        " +
                    "        }\r\n\r\n                // Authentication successful => Generate JWT token " +
                    "based on the user\'s claims\r\n                string token = this.GenerateJWT(clai" +
                    "ms);\r\n\r\n                // Return user with token\r\n                authenticated" +
                    "VM.User = mapper.Map<User, UserVM>(user);\r\n                authenticatedVM.Token" +
                    " = token;\r\n\r\n                return authenticatedVM;\r\n            }\r\n\r\n         " +
                    "   // Failed\r\n            //if (signInResult.RequiresTwoFactor)\r\n            //{" +
                    "\r\n            //    logger.LogInformation(\"User requires two factor auth\", user)" +
                    ";\r\n            //\r\n            //    return RedirectToAction(nameof(LoginWith2fa" +
                    "), new { returnUrl, loginVM.RememberMe });\r\n            //}\r\n            if (sig" +
                    "nInResult.IsLockedOut)\r\n            {\r\n                logger.LogWarning(\"User i" +
                    "s locked out\", user);\r\n                \r\n                throw new LoginFailedEx" +
                    "ception(\"locked-out\");\r\n            }\r\n            else if (signInResult.IsNotAl" +
                    "lowed)\r\n            {\r\n                logger.LogWarning(\"User is not allowed to" +
                    " login\", user);\r\n\r\n                throw new LoginFailedException(\"not-allowed\")" +
                    ";\r\n            }\r\n\r\n            logger.LogWarning(\"User login is invalid\", user)" +
                    ";\r\n\r\n            throw new LoginFailedException(\"invalid\");\r\n        }\r\n\r\n      " +
                    "  public async Task Logout()\r\n        {\r\n            await signInManager.SignOut" +
                    "Async();\r\n        }\r\n\r\n        public async Task<User> Me()\r\n        {\r\n        " +
                    "    User currentUser = await userManager.GetUserAsync(this.httpContextAccessor.H" +
                    "ttpContext.User);\r\n\r\n            // Retrieve roles of user\r\n            currentU" +
                    "ser.Roles = (List<string>)await userManager.GetRolesAsync(currentUser);\r\n\r\n     " +
                    "       return currentUser;\r\n        }\r\n\r\n        public async Task<RegisteredVM>" +
                    " Register(RegisterVM registerVM)\r\n        {\r\n            // Validation\r\n        " +
                    "    if (registerVM == null)\r\n            {\r\n                return null;\r\n      " +
                    "      }\r\n\r\n            // Result\r\n            RegisteredVM registeredVM = new Re" +
                    "gisteredVM();\r\n\r\n            User user = new User()\r\n            {\r\n            " +
                    "    UserName = registerVM.Username,\r\n                Email = registerVM.Email,\r\n" +
                    "                FirstName = registerVM.FirstName,\r\n                LastName = re" +
                    "gisterVM.LastName\r\n            };\r\n\r\n            IdentityResult result = await u" +
                    "serManager.CreateAsync(user, registerVM.Password);\r\n\r\n            if (result.Suc" +
                    "ceeded)\r\n            {\r\n                logger.LogInformation(\"User created a ne" +
                    "w account with password.\");\r\n\r\n                // Email confirmation\r\n          " +
                    "      if (configuration.GetSection(\"Authentication\").GetValue<bool>(\"EmailConfir" +
                    "mation\"))\r\n                {\r\n                    string code = await userManage" +
                    "r.GenerateEmailConfirmationTokenAsync(user);\r\n\r\n                    string callb" +
                    "ackUrl = configuration.GetSection(\"Authentication\").GetValue<string>(\"ConfirmEma" +
                    "ilURL\");\r\n                    callbackUrl = callbackUrl.Replace(\"{{userId}}\", us" +
                    "er.Id.ToString().ToUpper());\r\n                    callbackUrl = callbackUrl.Repl" +
                    "ace(\"{{userEmail}}\", user.Email.ToString().ToLower());\r\n                    call" +
                    "backUrl = callbackUrl.Replace(\"{{code}}\", Uri.EscapeDataString(code));\r\n\r\n      " +
                    "              await emailService.SendEmailConfirmationAsync(registerVM.Email, ca" +
                    "llbackUrl);\r\n                }\r\n                else\r\n                {\r\n       " +
                    "             // Set claims of user\r\n                    List<Claim> claims = new" +
                    " List<Claim>() {\r\n                        new Claim(JwtRegisteredClaimNames.Name" +
                    "Id, user.Id.ToString().ToUpper()),\r\n                        new Claim(JwtRegiste" +
                    "redClaimNames.UniqueName, user.UserName),\r\n                        new Claim(Jwt" +
                    "RegisteredClaimNames.Email, user.Email),\r\n                        new Claim(JwtR" +
                    "egisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture))\r\n" +
                    "                    };\r\n\r\n                    // TODO: Custom fields\r\n          " +
                    "          if (!string.IsNullOrEmpty(user.FirstName))\r\n                    {\r\n   " +
                    "                     claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, use" +
                    "r.FirstName));\r\n                    }\r\n                    if (!string.IsNullOrE" +
                    "mpty(user.LastName))\r\n                    {\r\n                        claims.Add(" +
                    "new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName));\r\n                " +
                    "    }\r\n\r\n                    // Registration successful, no email confirmation r" +
                    "equired => Generate JWT token based on the user\'s claims\r\n                    st" +
                    "ring token = this.GenerateJWT(claims);\r\n\r\n                    registeredVM.Token" +
                    " = token;\r\n                }\r\n\r\n                registeredVM.User = mapper.Map<U" +
                    "ser, UserVM>(user);\r\n\r\n                return registeredVM;\r\n            }\r\n\r\n  " +
                    "          logger.LogWarning(\"User registration is invalid\", user);\r\n\r\n          " +
                    "  throw new RegistrationFailedException(\"invalid\");\r\n        }\r\n\r\n        public" +
                    " async Task<EmailConfirmedVM> ConfirmEmail(string userId, string code)\r\n        " +
                    "{\r\n            // Validation\r\n            if (string.IsNullOrEmpty(userId) || st" +
                    "ring.IsNullOrEmpty(code))\r\n            {\r\n                return null;\r\n        " +
                    "    }\r\n\r\n            // Result\r\n            EmailConfirmedVM emailConfirmedVM = " +
                    "new EmailConfirmedVM();\r\n\r\n            User user = await userManager.FindByIdAsy" +
                    "nc(userId);\r\n            if (user == null)\r\n            {\r\n                logge" +
                    "r.LogWarning(\"User not found during email confirmation\", userId);\r\n\r\n           " +
                    "     throw new ConfirmEmailFailedException(\"invalid\");\r\n            }\r\n\r\n       " +
                    "     IdentityResult result = await userManager.ConfirmEmailAsync(user, code);\r\n " +
                    "           if (result.Succeeded)\r\n            {\r\n                // Set claims o" +
                    "f user\r\n                List<Claim> claims = new List<Claim>() {\r\n              " +
                    "      new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString().ToUpper()),\r\n" +
                    "                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)" +
                    ",\r\n                    new Claim(JwtRegisteredClaimNames.Email, user.Email),\r\n  " +
                    "                  new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToStrin" +
                    "g(CultureInfo.CurrentCulture))\r\n                };\r\n\r\n                // TODO: C" +
                    "ustom fields\r\n                if (!string.IsNullOrEmpty(user.FirstName))\r\n      " +
                    "          {\r\n                    claims.Add(new Claim(JwtRegisteredClaimNames.Gi" +
                    "venName, user.FirstName));\r\n                }\r\n                if (!string.IsNul" +
                    "lOrEmpty(user.LastName))\r\n                {\r\n                    claims.Add(new " +
                    "Claim(JwtRegisteredClaimNames.FamilyName, user.LastName));\r\n                }\r\n\r" +
                    "\n                // Email confirmation successful => Generate JWT token based on" +
                    " the user\'s claims\r\n                string token = this.GenerateJWT(claims);\r\n\r\n" +
                    "                emailConfirmedVM.Token = token;\r\n                emailConfirmedV" +
                    "M.User = mapper.Map<User, UserVM>(user);\r\n\r\n                return emailConfirme" +
                    "dVM;\r\n            }\r\n\r\n            logger.LogWarning(\"Email confirmation is inva" +
                    "lid\", user);\r\n\r\n            throw new ConfirmEmailFailedException(\"invalid\");\r\n " +
                    "       }\r\n\r\n        public async Task ForgotPassword(ForgotPasswordVM forgotPass" +
                    "wordVM)\r\n        {\r\n            // Validation\r\n            if (forgotPasswordVM " +
                    "== null)\r\n            {\r\n                throw new ForgotPasswordFailedException" +
                    "(\"invalid\");\r\n            }\r\n\r\n            // Retrieve user by email\r\n          " +
                    "  User user = await userManager.FindByEmailAsync(forgotPasswordVM.Email);\r\n     " +
                    "       if (user == null)\r\n            {\r\n                logger.LogWarning(\"User" +
                    " not found during forgot password\", forgotPasswordVM.Email);\r\n\r\n                " +
                    "throw new ForgotPasswordFailedException(\"invalid\");\r\n            }\r\n\r\n          " +
                    "  // For more information on how to enable account confirmation and password res" +
                    "et please\r\n            // visit https://go.microsoft.com/fwlink/?LinkID=532713\r\n" +
                    "\r\n            string code = await userManager.GeneratePasswordResetTokenAsync(us" +
                    "er);\r\n\r\n            logger.LogInformation(\"Password reset code:\");\r\n            " +
                    "logger.LogInformation(code);\r\n\r\n            var callbackUrl = configuration.GetS" +
                    "ection(\"Authentication\").GetValue<string>(\"ResetPasswordURL\");\r\n            call" +
                    "backUrl = callbackUrl.Replace(\"{{userId}}\", user.Id.ToString().ToUpper());\r\n    " +
                    "        callbackUrl = callbackUrl.Replace(\"{{userEmail}}\", user.Email.ToString()" +
                    ".ToLower());\r\n            callbackUrl = callbackUrl.Replace(\"{{code}}\", Uri.Esca" +
                    "peDataString(code));\r\n\r\n            await emailService.SendPasswordResetAsync(fo" +
                    "rgotPasswordVM.Email, callbackUrl);\r\n        }\r\n\r\n        public async Task<Pass" +
                    "wordResettedVM> ResetPassword(ResetPasswordVM resetPasswordVM)\r\n        {\r\n     " +
                    "       // Validation\r\n            if (resetPasswordVM == null)\r\n            {\r\n " +
                    "               throw new ResetPasswordFailedException(\"invalid\");\r\n            }" +
                    "\r\n\r\n            // Result\r\n            PasswordResettedVM passwordResettedVM = n" +
                    "ew PasswordResettedVM();\r\n\r\n            // Retrieve user by email\r\n            U" +
                    "ser user = await userManager.FindByIdAsync(resetPasswordVM.Id);\r\n            if " +
                    "(user == null)\r\n            {\r\n                logger.LogWarning(\"User not found" +
                    " during reset password\", resetPasswordVM.Id);\r\n\r\n                throw new Reset" +
                    "PasswordFailedException(\"invalid\");\r\n            }\r\n            \r\n            //" +
                    " Validate email address\r\n            if (user.Email != resetPasswordVM.Email)\r\n " +
                    "           {\r\n                throw new ResetPasswordFailedException(\"invalid-em" +
                    "ail\");\r\n            }\r\n\r\n            IdentityResult result = await userManager.R" +
                    "esetPasswordAsync(user, resetPasswordVM.Code, resetPasswordVM.Password);\r\n      " +
                    "      if (result.Succeeded)\r\n            {\r\n                // Set claims of use" +
                    "r\r\n                List<Claim> claims = new List<Claim>() {\r\n                   " +
                    " new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString().ToUpper()),\r\n     " +
                    "               new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),\r\n  " +
                    "                  new Claim(JwtRegisteredClaimNames.Email, user.Email),\r\n       " +
                    "             new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(Cul" +
                    "tureInfo.CurrentCulture))\r\n                };\r\n\r\n                // TODO: Custom" +
                    " fields\r\n                if (!string.IsNullOrEmpty(user.FirstName))\r\n           " +
                    "     {\r\n                    claims.Add(new Claim(JwtRegisteredClaimNames.GivenNa" +
                    "me, user.FirstName));\r\n                }\r\n                if (!string.IsNullOrEm" +
                    "pty(user.LastName))\r\n                {\r\n                    claims.Add(new Claim" +
                    "(JwtRegisteredClaimNames.FamilyName, user.LastName));\r\n                }\r\n\r\n    " +
                    "            // Registration successful, no email confirmation required => Genera" +
                    "te JWT token based on the user\'s claims\r\n                string token = this.Gen" +
                    "erateJWT(claims);\r\n\r\n                passwordResettedVM.Token = token;\r\n        " +
                    "        passwordResettedVM.User = mapper.Map<User, UserVM>(user);\r\n\r\n           " +
                    "     return passwordResettedVM;\r\n            }\r\n            \r\n            logger" +
                    ".LogWarning(\"Reset password is invalid\", user);\r\n\r\n            throw new ResetPa" +
                    "sswordFailedException(\"invalid\");\r\n        }\r\n\r\n        public string GenerateJW" +
                    "T(List<Claim> claims)\r\n        {\r\n            JwtSecurityTokenHandler tokenHandl" +
                    "er = new JwtSecurityTokenHandler();\r\n            var key = Encoding.ASCII.GetByt" +
                    "es(configuration.GetSection(\"Authentication\").GetValue<string>(\"Secret\"));\r\n    " +
                    "        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor\r\n " +
                    "           {\r\n                Subject = new ClaimsIdentity(claims),\r\n           " +
                    "     Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration.GetSection(" +
                    "\"Authentication\").GetValue<string>(\"TokenExpiresInMinutes\"))),\r\n                " +
                    "SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), Secur" +
                    "ityAlgorithms.HmacSha256Signature)\r\n            };\r\n\r\n            return tokenHa" +
                    "ndler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));\r\n        }\r\n    }\r\n" +
                    "}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class AuthBLLTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
