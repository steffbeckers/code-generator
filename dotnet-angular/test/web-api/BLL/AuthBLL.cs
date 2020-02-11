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

            // Retrieve user by email or username
            User currentUser = await userManager.FindByEmailAsync(model.EmailOrUsername) ?? await userManager.FindByNameAsync(model.EmailOrUsername);
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