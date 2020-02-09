﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CodeGenCLI.Templates.WebAPI.Controllers
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
    
    #line 1 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\Controllers\AuthControllerTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class AuthControllerTemplate : AuthControllerTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using AutoMapper;\r\nusing ");
            
            #line 9 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\Controllers\AuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".Models;\r\nusing ");
            
            #line 10 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\Controllers\AuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".Services;\r\nusing ");
            
            #line 11 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\Controllers\AuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(@".ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

namespace ");
            
            #line 26 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\Controllers\AuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".Controllers\r\n{\r\n    [Authorize]\r\n    [Route(\"api/[controller]\")]\r\n    [ApiContro" +
                    "ller]\r\n    public class AuthController : ControllerBase\r\n    {\r\n        private " +
                    "readonly UserManager<User> userManager;\r\n        private readonly SignInManager<" +
                    "User> signInManager;\r\n        private readonly IEmailService emailService;\r\n    " +
                    "    private readonly ILogger logger;\r\n        private readonly IConfiguration co" +
                    "nfiguration;\r\n        private readonly IMapper mapper;\r\n\r\n        public AuthCon" +
                    "troller(\r\n            UserManager<User> userManager,\r\n            SignInManager<" +
                    "User> signInManager,\r\n            IEmailService emailService,\r\n            ILogg" +
                    "er<AuthController> logger,\r\n            IConfiguration configuration,\r\n         " +
                    "   IMapper mapper)\r\n        {\r\n            this.userManager = userManager;\r\n    " +
                    "        this.signInManager = signInManager;\r\n            this.emailService = ema" +
                    "ilService;\r\n            this.logger = logger;\r\n            this.configuration = " +
                    "configuration;\r\n            this.mapper = mapper;\r\n        }\r\n\r\n        // TODO:" +
                    " Move this function to another layer?\r\n        private string GenerateJWT(List<C" +
                    "laim> claims)\r\n        {\r\n            JwtSecurityTokenHandler tokenHandler = new" +
                    " JwtSecurityTokenHandler();\r\n            var key = Encoding.ASCII.GetBytes(confi" +
                    "guration.GetSection(\"Authentication\").GetValue<string>(\"Secret\"));\r\n            " +
                    "SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor\r\n         " +
                    "   {\r\n                Subject = new ClaimsIdentity(claims),\r\n                Exp" +
                    "ires = DateTime.UtcNow.AddMinutes(double.Parse(configuration.GetSection(\"Authent" +
                    "ication\").GetValue<string>(\"TokenExpiresInMinutes\"))),\r\n                SigningC" +
                    "redentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgor" +
                    "ithms.HmacSha256Signature)\r\n            };\r\n\r\n            return tokenHandler.Wr" +
                    "iteToken(tokenHandler.CreateToken(tokenDescriptor));\r\n        }\r\n\r\n        [Http" +
                    "Post]\r\n        [Route(\"login\")]\r\n        [AllowAnonymous]\r\n        public async " +
                    "Task<IActionResult> Login([FromBody] LoginVM model)\r\n        {\r\n            if (" +
                    "ModelState.IsValid)\r\n            {\r\n                // Retrieve user by email or" +
                    " username\r\n                User currentUser = await userManager.FindByEmailAsync" +
                    "(model.EmailOrUsername) ?? await userManager.FindByNameAsync(model.EmailOrUserna" +
                    "me);\r\n\r\n                // If no user is found by email or username, just return" +
                    " unauthorized and give nothing away of existing user info\r\n                if (c" +
                    "urrentUser == null)\r\n                {\r\n                    return Unauthorized(" +
                    "\"invalid\");\r\n                }\r\n\r\n                // Log the user in by password" +
                    "\r\n                var result = await signInManager.PasswordSignInAsync(currentUs" +
                    "er, model.Password, model.RememberMe, lockoutOnFailure: true);\r\n                " +
                    "if (result.Succeeded)\r\n                {\r\n                    logger.LogInformat" +
                    "ion(\"User \" + currentUser.Id + \" logged in.\");\r\n\r\n                    // Retriev" +
                    "e roles of user\r\n                    currentUser.Roles = (List<string>)await use" +
                    "rManager.GetRolesAsync(currentUser);\r\n\r\n                    // Set claims of use" +
                    "r\r\n                    List<Claim> claims = new List<Claim>() {\r\n               " +
                    "         new Claim(JwtRegisteredClaimNames.NameId, currentUser.Id.ToString().ToU" +
                    "pper()),\r\n                        new Claim(JwtRegisteredClaimNames.UniqueName, " +
                    "currentUser.UserName),\r\n                        new Claim(JwtRegisteredClaimName" +
                    "s.Email, currentUser.Email),\r\n                        new Claim(JwtRegisteredCla" +
                    "imNames.Iat, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture))\r\n            " +
                    "        };\r\n                    if (!string.IsNullOrEmpty(currentUser.FirstName)" +
                    ")\r\n                    {\r\n                        claims.Add(new Claim(JwtRegist" +
                    "eredClaimNames.GivenName, currentUser.FirstName));\r\n                    }\r\n     " +
                    "               if (!string.IsNullOrEmpty(currentUser.LastName))\r\n               " +
                    "     {\r\n                        claims.Add(new Claim(JwtRegisteredClaimNames.Fam" +
                    "ilyName, currentUser.LastName));\r\n                    }\r\n\r\n                    /" +
                    "/ Add roles as claims\r\n                    foreach (var role in currentUser.Role" +
                    "s)\r\n                    {\r\n                        claims.Add(new Claim(ClaimTyp" +
                    "es.Role, role));\r\n                    }\r\n\r\n                    // Authentication" +
                    " successful => Generate JWT token based on the user\'s claims\r\n                  " +
                    "  string token = GenerateJWT(claims);\r\n\r\n                    // Return user with" +
                    " token\r\n                    return Ok(new AuthenticatedVM()\r\n                   " +
                    " {\r\n                        User = mapper.Map<User, UserVM>(currentUser),\r\n     " +
                    "                   Token = token,\r\n                        RememberMe = model.Re" +
                    "memberMe\r\n                    });\r\n                }\r\n                //if (resu" +
                    "lt.RequiresTwoFactor)\r\n                //{\r\n                //    logger.LogInfo" +
                    "rmation(\"Requires two factor.\");\r\n                //    return RedirectToAction(" +
                    "nameof(LoginWith2fa), new { returnUrl, model.RememberMe });\r\n                //}" +
                    "\r\n                if (result.IsLockedOut)\r\n                {\r\n                  " +
                    "  // INFO: This is possible to split some code\r\n                    //return Red" +
                    "irectToAction(nameof(Lockout));\r\n\r\n                    logger.LogWarning(\"User i" +
                    "s locked out.\");\r\n                    return Unauthorized(\"locked-out\");\r\n      " +
                    "          }\r\n                if (result.IsNotAllowed)\r\n                {\r\n      " +
                    "              logger.LogWarning(\"User is not allowed to login.\");\r\n             " +
                    "       return Unauthorized(\"not-allowed\");\r\n                }\r\n                e" +
                    "lse\r\n                {\r\n                    logger.LogWarning(\"Invalid login att" +
                    "empt.\");\r\n                    return Unauthorized(\"invalid\");\r\n                }" +
                    "\r\n            }\r\n\r\n            // If we got this far, something failed\r\n        " +
                    "    return BadRequest();\r\n        }\r\n\r\n        [HttpGet]\r\n        [Route(\"me\")]\r" +
                    "\n        public async Task<IActionResult> Me()\r\n        {\r\n            User curr" +
                    "entUser = await userManager.GetUserAsync(User);\r\n\r\n            // Retrieve roles" +
                    " of user\r\n            currentUser.Roles = (List<string>)await userManager.GetRol" +
                    "esAsync(currentUser);\r\n\r\n            return Ok(mapper.Map<User, UserVM>(currentU" +
                    "ser));\r\n        }\r\n\r\n        [HttpPost]\r\n        [Route(\"register\")]\r\n        [A" +
                    "llowAnonymous]\r\n        public async Task<IActionResult> Register([FromBody] Reg" +
                    "isterVM model)\r\n        {\r\n            if (ModelState.IsValid)\r\n            {\r\n " +
                    "               var user = new User { UserName = model.Username, Email = model.Em" +
                    "ail };\r\n\r\n                var result = await userManager.CreateAsync(user, model" +
                    ".Password);\r\n\r\n                if (result.Succeeded)\r\n                {\r\n       " +
                    "             logger.LogInformation(\"User created a new account with password.\");" +
                    "\r\n\r\n                    var code = await userManager.GenerateEmailConfirmationTo" +
                    "kenAsync(user);\r\n                    var callbackUrl = Url.EmailConfirmationLink" +
                    "(user.Id.ToString().ToUpper(), code, Request.Scheme);\r\n                    await" +
                    " emailService.SendEmailConfirmationAsync(model.Email, callbackUrl);\r\n\r\n         " +
                    "           // When self registering and login at the same time\r\n                " +
                    "    // Need to add/refactor JWT logic if adding\r\n                    //await sig" +
                    "nInManager.SignInAsync(user, isPersistent: false);\r\n\r\n                    return" +
                    " Ok();\r\n                }\r\n\r\n                AddErrors(result);\r\n            }\r\n" +
                    "\r\n            // If we got this far, something failed\r\n            return BadReq" +
                    "uest(ModelState);\r\n        }\r\n\r\n        [HttpGet]\r\n        [Route(\"logout\")]\r\n  " +
                    "      public async Task<IActionResult> Logout()\r\n        {\r\n            await si" +
                    "gnInManager.SignOutAsync();\r\n\r\n            return Ok();\r\n        }\r\n\r\n        [H" +
                    "ttpGet]\r\n        [Route(\"confirm-email\")]\r\n        [AllowAnonymous]\r\n        pub" +
                    "lic async Task<IActionResult> ConfirmEmail(string userId, string code)\r\n        " +
                    "{\r\n            if (userId == null || code == null)\r\n            {\r\n             " +
                    "   return BadRequest(ModelState);\r\n            }\r\n\r\n            var user = await" +
                    " userManager.FindByIdAsync(userId);\r\n            if (user == null)\r\n            " +
                    "{\r\n                throw new ApplicationException($\"Unable to load user with ID " +
                    "\'{userId}\'.\");\r\n            }\r\n\r\n            var result = await userManager.Conf" +
                    "irmEmailAsync(user, code);\r\n            if (result.Succeeded)\r\n            {\r\n  " +
                    "              return Ok();\r\n            }\r\n\r\n            AddErrors(result);\r\n\r\n " +
                    "           // If we got this far, something failed\r\n            return BadReques" +
                    "t(ModelState);\r\n        }\r\n\r\n        [HttpPost]\r\n        [Route(\"forgot-password" +
                    "\")]\r\n        [AllowAnonymous]\r\n        public async Task<IActionResult> ForgotPa" +
                    "ssword([FromBody] ForgotPasswordVM model)\r\n        {\r\n            if (ModelState" +
                    ".IsValid)\r\n            {\r\n                var user = await userManager.FindByEma" +
                    "ilAsync(model.Email);\r\n                if (user == null)\r\n                {\r\n   " +
                    "                 //// Don\'t reveal that the user does not exist\r\n               " +
                    "     //return Ok();\r\n\r\n                    // For CRM purposes\r\n                " +
                    "    return NotFound(\"email-not-found\");\r\n                }\r\n\r\n                //" +
                    " Check if email is confirmed, if required in Startup settings\r\n                /" +
                    "/ In startup: options.SignIn.RequireConfirmedEmail = true;\r\n                if (" +
                    "!(await userManager.IsEmailConfirmedAsync(user)))\r\n                {\r\n          " +
                    "          //// Don\'t reveal that the user does not exist\r\n                    //" +
                    "return Ok();\r\n\r\n                    // OR\r\n\r\n                    return NotFound" +
                    "(\"email-not-confirmed\");\r\n                }\r\n\r\n                // For more infor" +
                    "mation on how to enable account confirmation and password reset please\r\n        " +
                    "        // visit https://go.microsoft.com/fwlink/?LinkID=532713\r\n\r\n             " +
                    "   var code = await userManager.GeneratePasswordResetTokenAsync(user);\r\n\r\n      " +
                    "          //var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Reque" +
                    "st.Scheme);\r\n                var callbackUrl = configuration.GetSection(\"EmailSe" +
                    "ttings\").GetValue<string>(\"PasswordResetURL\");\r\n                callbackUrl = ca" +
                    "llbackUrl.Replace(\"{{userId}}\", user.Id.ToString().ToLower());\r\n                " +
                    "callbackUrl = callbackUrl.Replace(\"{{userEmail}}\", user.Email.ToString().ToLower" +
                    "());\r\n                callbackUrl = callbackUrl.Replace(\"{{code}}\", Uri.EscapeDa" +
                    "taString(code));\r\n\r\n                await emailService.SendEmailAsync(model.Emai" +
                    "l, \"Reset Password\",\r\n                   $\"Please reset your password by clickin" +
                    "g here: <a href=\'{callbackUrl}\'>link</a>\");\r\n\r\n                return Ok();\r\n   " +
                    "         }\r\n\r\n            // If we got this far, something failed\r\n            r" +
                    "eturn BadRequest(ModelState);\r\n        }\r\n\r\n        [HttpPost]\r\n        [Route(\"" +
                    "reset-password\")]\r\n        [AllowAnonymous]\r\n        public async Task<IActionRe" +
                    "sult> ResetPassword([FromBody] ResetPasswordVM model)\r\n        {\r\n            if" +
                    " (ModelState.IsValid)\r\n            {\r\n                var user = await userManag" +
                    "er.FindByIdAsync(model.Id);\r\n                if (user == null)\r\n                " +
                    "{\r\n                    //return BadRequest();\r\n\r\n                    // OR\r\n\r\n  " +
                    "                  // For CRM purposes\r\n                    return NotFound(\"user" +
                    "-not-found\");\r\n                }\r\n                if (user.Email != model.Email)" +
                    "\r\n                {\r\n                    //return BadRequest();\r\n\r\n             " +
                    "       // OR\r\n\r\n                    // For CRM purposes\r\n                    ret" +
                    "urn BadRequest(\"email-does-not-match\");\r\n                }\r\n\r\n                va" +
                    "r result = await userManager.ResetPasswordAsync(user, model.Code, model.Password" +
                    ");\r\n                if (result.Succeeded)\r\n                {\r\n                  " +
                    "  // TODO: Maybe log the user in automatically? Need to add/refactor JWT logic i" +
                    "f adding\r\n                    //await signInManager.SignInAsync(user, isPersiste" +
                    "nt: false);\r\n\r\n                    return Ok();\r\n                }\r\n\r\n          " +
                    "      AddErrors(result);\r\n            }\r\n\r\n            return BadRequest(ModelSt" +
                    "ate);\r\n        }\r\n\r\n        #region Helpers\r\n\r\n        private void AddErrors(Id" +
                    "entityResult result)\r\n        {\r\n            foreach (var error in result.Errors" +
                    ")\r\n            {\r\n                ModelState.AddModelError(string.Empty, error.D" +
                    "escription);\r\n            }\r\n        }\r\n\r\n        #endregion\r\n    }\r\n}\r\n");
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
    public class AuthControllerTemplateBase
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
