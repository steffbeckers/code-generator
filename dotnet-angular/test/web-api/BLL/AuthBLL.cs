using AutoMapper;
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
using RJM.API.Framework.Exceptions;
using RJM.API.Models;
using RJM.API.Services;
using RJM.API.ViewModels.Identity;

namespace RJM.API.BLL
{
	/// <summary>
	/// The business logic layer for authentication.
	/// </summary>
    public class AuthBLL
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailService emailService;

        // #-#-#
        // Test

        public AuthBLL(
            IConfiguration configuration,
            ILogger<AuthBLL> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailService emailService
        )
        {
            this.configuration = configuration;
            this.logger = logger;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
        }

        // #-#-#
        // Test 2

        public async Task<AuthenticatedVM> Login(LoginVM loginVM)
        {
            // Validation
            if (loginVM == null)
            {
                return null;
            }

            // Result
            AuthenticatedVM authenticatedVM = new AuthenticatedVM()
            {
                RememberMe = loginVM.RememberMe
            };

            // Retrieve user by email or username
            User user = await userManager.FindByEmailAsync(loginVM.EmailOrUsername) ?? await userManager.FindByNameAsync(loginVM.EmailOrUsername);
            if (user == null)
            {
                logger.LogWarning("User not found during login", loginVM.EmailOrUsername);

                throw new LoginFailedException("invalid");
            }

            // Log the user in by password
            SignInResult signInResult = await signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: true);
            
            // Success
            if (signInResult.Succeeded)
            {
                // Authenticated by password
                logger.LogInformation("User logged in", user);

                // Retrieve roles of user
                user.Roles = (List<string>)await userManager.GetRolesAsync(user);

                // Set claims of user
                List<Claim> claims = new List<Claim>() {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString().ToUpper()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture))
                };

                // TODO: Custom fields
                if (!string.IsNullOrEmpty(user.FirstName))
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName));
                }
                if (!string.IsNullOrEmpty(user.LastName))
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName));
                }

                // Add roles as claims
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Authentication successful => Generate JWT token based on the user's claims
                string token = this.GenerateJWT(claims);

                // Return user with token
                authenticatedVM.User = mapper.Map<User, UserVM>(user);
                authenticatedVM.Token = token;

                return authenticatedVM;
            }

            // Failed
            //if (signInResult.RequiresTwoFactor)
            //{
            //    logger.LogInformation("User requires two factor auth", user);
            //
            //    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, loginVM.RememberMe });
            //}
            if (signInResult.IsLockedOut)
            {
                logger.LogWarning("User is locked out", user);
                
                throw new LoginFailedException("locked-out");
            }
            else if (signInResult.IsNotAllowed)
            {
                logger.LogWarning("User is not allowed to login", user);

                throw new LoginFailedException("not-allowed");
            }

            logger.LogWarning("User login is invalid", user);

            throw new LoginFailedException("invalid");
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<User> Me()
        {
            User currentUser = await userManager.GetUserAsync(this.httpContextAccessor.HttpContext.User);

            // Retrieve roles of user
            currentUser.Roles = (List<string>)await userManager.GetRolesAsync(currentUser);

            return currentUser;
        }

        public async Task<RegisteredVM> Register(RegisterVM registerVM)
        {
            // Validation
            if (registerVM == null)
            {
                return null;
            }

            // Result
            RegisteredVM registeredVM = new RegisteredVM();

            User user = new User()
            {
                UserName = registerVM.Username,
                Email = registerVM.Email,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName
            };

            IdentityResult result = await userManager.CreateAsync(user, registerVM.Password);

            if (result.Succeeded)
            {
                logger.LogInformation("User created a new account with password.");

                // Email confirmation
                if (configuration.GetSection("Authentication").GetValue<bool>("EmailConfirmation"))
                {
                    string code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    string callbackUrl = configuration.GetSection("Authentication").GetValue<string>("ConfirmEmailURL");
                    callbackUrl = callbackUrl.Replace("{{userId}}", user.Id.ToString().ToUpper());
                    callbackUrl = callbackUrl.Replace("{{userEmail}}", user.Email.ToString().ToLower());
                    callbackUrl = callbackUrl.Replace("{{code}}", Uri.EscapeDataString(code));

                    await emailService.SendEmailConfirmationAsync(registerVM.Email, callbackUrl);
                }
                else
                {
                    // Set claims of user
                    List<Claim> claims = new List<Claim>() {
                        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString().ToUpper()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture))
                    };

                    // TODO: Custom fields
                    if (!string.IsNullOrEmpty(user.FirstName))
                    {
                        claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName));
                    }
                    if (!string.IsNullOrEmpty(user.LastName))
                    {
                        claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName));
                    }

                    // Registration successful, no email confirmation required => Generate JWT token based on the user's claims
                    string token = this.GenerateJWT(claims);

                    registeredVM.Token = token;
                }

                registeredVM.User = mapper.Map<User, UserVM>(user);

                return registeredVM;
            }

            logger.LogWarning("User registration is invalid", user);

            throw new RegistrationFailedException("invalid");
        }

        public async Task<EmailConfirmedVM> ConfirmEmail(string userId, string code)
        {
            // Validation
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
            {
                return null;
            }

            // Result
            EmailConfirmedVM emailConfirmedVM = new EmailConfirmedVM();

            User user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                logger.LogWarning("User not found during email confirmation", userId);

                throw new ConfirmEmailFailedException("invalid");
            }

            IdentityResult result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                // Set claims of user
                List<Claim> claims = new List<Claim>() {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString().ToUpper()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture))
                };

                // TODO: Custom fields
                if (!string.IsNullOrEmpty(user.FirstName))
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName));
                }
                if (!string.IsNullOrEmpty(user.LastName))
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName));
                }

                // Email confirmation successful => Generate JWT token based on the user's claims
                string token = this.GenerateJWT(claims);

                emailConfirmedVM.Token = token;
                emailConfirmedVM.User = mapper.Map<User, UserVM>(user);

                return emailConfirmedVM;
            }

            logger.LogWarning("Email confirmation is invalid", user);

            throw new ConfirmEmailFailedException("invalid");
        }

        public async Task ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            // Validation
            if (forgotPasswordVM == null)
            {
                throw new ForgotPasswordFailedException("invalid");
            }

            // Retrieve user by email
            User user = await userManager.FindByEmailAsync(forgotPasswordVM.Email);
            if (user == null)
            {
                logger.LogWarning("User not found during forgot password", forgotPasswordVM.Email);

                throw new ForgotPasswordFailedException("invalid");
            }

            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713

            string code = await userManager.GeneratePasswordResetTokenAsync(user);

            logger.LogInformation("Password reset code:");
            logger.LogInformation(code);

            var callbackUrl = configuration.GetSection("Authentication").GetValue<string>("ResetPasswordURL");
            callbackUrl = callbackUrl.Replace("{{userId}}", user.Id.ToString().ToUpper());
            callbackUrl = callbackUrl.Replace("{{userEmail}}", user.Email.ToString().ToLower());
            callbackUrl = callbackUrl.Replace("{{code}}", Uri.EscapeDataString(code));

            await emailService.SendPasswordResetAsync(forgotPasswordVM.Email, callbackUrl);
        }

        public async Task<PasswordResettedVM> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            // Validation
            if (resetPasswordVM == null)
            {
                throw new ResetPasswordFailedException("invalid");
            }

            // Result
            PasswordResettedVM passwordResettedVM = new PasswordResettedVM();

            // Retrieve user by email
            User user = await userManager.FindByIdAsync(resetPasswordVM.Id);
            if (user == null)
            {
                logger.LogWarning("User not found during reset password", resetPasswordVM.Id);

                throw new ResetPasswordFailedException("invalid");
            }
            
            // Validate email address
            if (user.Email != resetPasswordVM.Email)
            {
                throw new ResetPasswordFailedException("invalid-email");
            }

            IdentityResult result = await userManager.ResetPasswordAsync(user, resetPasswordVM.Code, resetPasswordVM.Password);
            if (result.Succeeded)
            {
                // Set claims of user
                List<Claim> claims = new List<Claim>() {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString().ToUpper()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture))
                };

                // TODO: Custom fields
                if (!string.IsNullOrEmpty(user.FirstName))
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName));
                }
                if (!string.IsNullOrEmpty(user.LastName))
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName));
                }

                // Registration successful, no email confirmation required => Generate JWT token based on the user's claims
                string token = this.GenerateJWT(claims);

                passwordResettedVM.Token = token;
                passwordResettedVM.User = mapper.Map<User, UserVM>(user);

                return passwordResettedVM;
            }
            
            logger.LogWarning("Reset password is invalid", user);

            throw new ResetPasswordFailedException("invalid");
        }

        public string GenerateJWT(List<Claim> claims)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Authentication").GetValue<string>("Secret"));
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration.GetSection("Authentication").GetValue<string>("TokenExpiresInMinutes"))),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}