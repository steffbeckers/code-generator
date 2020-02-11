using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;
using Test.API.ViewModels.Identity;
using Test.API.Services;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for authentication.
	/// </summary>
    public class AuthBLL
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailService emailService;

        public AuthBLL(
            IConfiguration configuration,
            ILogger<AuthBLL> logger,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailService emailService
        )
        {
            this.configuration = configuration;
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
        }

        public async Task<LoginResultVM> Login(LoginVM loginVM) {
            // Validation
            if (loginVM == null) {
                return null;
            }

            // Result
            LoginResultVM loginResultVM = new LoginResultVM() {
                RememberMe = loginVM.RememberMe
            };

            // Retrieve user by email or username
            User user = await userManager.FindByEmailAsync(loginVM.EmailOrUsername) ?? await userManager.FindByNameAsync(loginVM.EmailOrUsername);
        
            // If no user is found by email or username, just return unauthorized and give nothing away of existing user info
            if (user == null)
            {
                logger.LogWarning("User not found during login", loginVM.EmailOrUsername);

                loginResultVM.Error = "invalid";
                return loginResultVM;
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
                return new LoginResultVM() {
                    Authenticated = {
                        User = mapper.Map<User, UserVM>(currentUser),
                        Token = token
                    }
                };
            }

            // Failed
            //if (signInResult.RequiresTwoFactor)
            //{
            //    logger.LogInformation("User requires two factor auth", user);
            //
            //    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
            //}
            if (signInResult.IsLockedOut)
            {
                logger.LogWarning("User is locked out", user);
                
                loginResultVM.Error = "locked-out";
            }
            if (signInResult.IsNotAllowed)
            {
                logger.LogWarning("User is not allowed to login", user);

                loginResultVM.Error = "not-allowed";
            }
            else
            {
                logger.LogWarning("User login is invalid", user);

                loginResultVM.Error = "invalid";
            }

            return loginResultVM;
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