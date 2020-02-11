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

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for authentication.
	/// </summary>
    public class AuthBLL
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailService emailService;

        public AuthBLL(
            IConfiguration configuration,
            ILogger<AuthBLL> logger,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailService emailService
        )
        {
            this.configuration = configuration;
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
        }

        public async Task<LoginResultVM> Login(LoginVM loginVM) {
            // Validation
            if (loginVM == null) {
                return null;
            }

            LoginResultVM loginResultVM = new LoginResultVM() {
                RememberMe = loginVM.RememberMe
            };

            // Retrieve user by email or username
            User user = await userManager.FindByEmailAsync(loginVM.EmailOrUsername) ?? await userManager.FindByNameAsync(loginVM.EmailOrUsername);
        
            // If no user is found by email or username, just return unauthorized and give nothing away of existing user info
            if (user == null)
            {
                loginResultVM.Errors.Add("invalid");
                return loginResultVM;
            }

            // Log the user in by password
            var result = await signInManager.PasswordSignInAsync(currentUser, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                logger.LogInformation("User " + currentUser.Id + " logged in.");

                // Retrieve roles of user
                currentUser.Roles = (List<string>)await userManager.GetRolesAsync(currentUser);

                // Set claims of user
                List<Claim> claims = new List<Claim>() {
                    new Claim(JwtRegisteredClaimNames.NameId, currentUser.Id.ToString().ToUpper()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, currentUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, currentUser.Email),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture))
                };
                if (!string.IsNullOrEmpty(currentUser.FirstName))
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, currentUser.FirstName));
                }
                if (!string.IsNullOrEmpty(currentUser.LastName))
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, currentUser.LastName));
                }

                // Add roles as claims
                foreach (var role in currentUser.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Authentication successful => Generate JWT token based on the user's claims
                string token = this.bll.GenerateJWT(claims);

                // Return user with token
                return Ok(new AuthenticatedVM()
                {
                    User = mapper.Map<User, UserVM>(currentUser),
                    Token = token,
                    RememberMe = model.RememberMe
                });
            }
            //if (result.RequiresTwoFactor)
            //{
            //    logger.LogInformation("Requires two factor.");
            //    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
            //}
            if (result.IsLockedOut)
            {
                // INFO: This is possible to split some code
                //return RedirectToAction(nameof(Lockout));

                logger.LogWarning("User is locked out.");
                return Unauthorized("locked-out");
            }
            if (result.IsNotAllowed)
            {
                logger.LogWarning("User is not allowed to login.");
                return Unauthorized("not-allowed");
            }
            else
            {
                logger.LogWarning("Invalid login attempt.");
                return Unauthorized("invalid");
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