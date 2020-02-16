﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CodeGenCLI.Templates.WebAPI.Services
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
    
    #line 1 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\Services\EmailServiceTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class EmailServiceTemplate : EmailServiceTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using MailKit.Net.Smtp;\r\nusing Microsoft.AspNetCore.Mvc;\r\nusing Microsoft.Extensi" +
                    "ons.Configuration;\r\nusing MimeKit;\r\nusing System.Text.Encodings.Web;\r\nusing Syst" +
                    "em.Threading.Tasks;\r\nusing ");
            
            #line 14 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\Services\EmailServiceTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".Controllers;\r\n\r\nnamespace ");
            
            #line 16 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\Services\EmailServiceTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(@".Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713 and https://kenhaggerty.com/articles/article/aspnet-core-22-smtp-emailsender-implementation

    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
");
            
            #line 24 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\Services\EmailServiceTemplate.tt"
 if (config.Authentication.Enabled) { 
            
            #line default
            #line hidden
            this.Write("        Task SendEmailConfirmationAsync(string email, string link);\r\n        Task" +
                    " SendPasswordResetAsync(string email, string link);\r\n");
            
            #line 27 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\Services\EmailServiceTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("    }\r\n\r\n    public class EmailService : IEmailService\r\n    {\r\n        private re" +
                    "adonly IConfiguration configuration;\r\n\r\n        public EmailService(IConfigurati" +
                    "on configuration)\r\n        {\r\n            this.configuration = configuration;\r\n " +
                    "       }\r\n\r\n        public async Task SendEmailAsync(string email, string subjec" +
                    "t, string message)\r\n        {\r\n            try\r\n            {\r\n                v" +
                    "ar mimeMessage = new MimeMessage();\r\n                mimeMessage.From.Add(new Ma" +
                    "ilboxAddress(configuration.GetSection(\"EmailService\").GetValue<string>(\"SenderNa" +
                    "me\"), configuration.GetSection(\"EmailService\").GetValue<string>(\"Sender\")));\r\n  " +
                    "              mimeMessage.To.Add(new MailboxAddress(email));\r\n                mi" +
                    "meMessage.Subject = subject;\r\n                mimeMessage.Body = new TextPart(\"h" +
                    "tml\")\r\n                {\r\n                    Text = message\r\n                };" +
                    "\r\n\r\n                using (var client = new SmtpClient())\r\n                {\r\n  " +
                    "                  // For demo-purposes, accept all SSL certificates (in case the" +
                    " server supports STARTTLS)\r\n                    client.ServerCertificateValidati" +
                    "onCallback = (s, c, h, e) => true;\r\n\r\n                    // The third parameter" +
                    " is useSSL (true if the client should make an SSL-wrapped\r\n                    /" +
                    "/ connection to the server; otherwise, false).\r\n                    await client" +
                    ".ConnectAsync(configuration.GetSection(\"EmailService\").GetValue<string>(\"MailSer" +
                    "ver\"), configuration.GetSection(\"EmailService\").GetValue<int>(\"MailPort\"), confi" +
                    "guration.GetSection(\"EmailService\").GetValue<bool>(\"UseSSL\"));\r\n\r\n              " +
                    "      // Note: only needed if the SMTP server requires authentication\r\n         " +
                    "           await client.AuthenticateAsync(configuration.GetSection(\"EmailService" +
                    "\").GetValue<string>(\"Sender\"), configuration.GetSection(\"EmailService\").GetValue" +
                    "<string>(\"Password\"));\r\n\r\n                    await client.SendAsync(mimeMessage" +
                    ");\r\n\r\n                    await client.DisconnectAsync(true);\r\n                }" +
                    "\r\n            }\r\n            catch\r\n            {\r\n                throw;\r\n     " +
                    "       }\r\n        }\r\n");
            
            #line 74 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\Services\EmailServiceTemplate.tt"
 if (config.Authentication.Enabled) { 
            
            #line default
            #line hidden
            this.Write(@"
        public Task SendEmailConfirmationAsync(string email, string link)
        {
            return SendEmailAsync(email, ""Confirm your email"",
                $""<p>Please confirm your account by clicking this link:</p><a href='{HtmlEncoder.Default.Encode(link)}'>{link}</a>"");
        }

        public Task SendPasswordResetAsync(string email, string link)
        {
            return SendEmailAsync(email, ""Reset your password"",
                $""<p>Please reset your password by clicking this link:</p><a href='{HtmlEncoder.Default.Encode(link)}'>{link}</a>"");
        }
");
            
            #line 87 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\Services\EmailServiceTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("    }\r\n}\r\n");
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
    public class EmailServiceTemplateBase
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
