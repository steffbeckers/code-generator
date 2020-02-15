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
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Test.API.Framework.Exceptions;

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
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailService emailService;

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

        public async Task<AuthenticatedVM> Login(LoginVM loginVM) {
            // Validation
            if (loginVM == null) {
                return null;
            }

            // Result
            AuthenticatedVM authenticatedVM = new AuthenticatedVM() {
                RememberMe = loginVM.RememberMe
            };

            // Retrieve user by email or username
            User user = await userManager.FindByEmailAsync(loginVM.EmailOrUsername) ?? await userManager.FindByNameAsync(loginVM.EmailOrUsername);
        
            // If no user is found by email or username, just return unauthorized and give nothing away of existing user info
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
                authenticatedVM = new AuthenticatedVM()
                {
                    User = mapper.Map<User, UserVM>(user),
                    Token = token
                };

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

        public async Task<User> Me()
        {
            User currentUser = await userManager.GetUserAsync(this.httpContextAccessor.HttpContext.User);

            // Retrieve roles of user
            currentUser.Roles = (List<string>)await userManager.GetRolesAsync(currentUser);

            return currentUser;
        }

        public async Task<RegisteredVM> Register(RegisterVM registerVM) {
            // Validation
            if (registerVM == null) {
                return null;
            }

            // Result
            RegisteredVM registeredVM = new RegisteredVM();

            User user = new User {
                UserName = registerVM.Username,
                Email = registerVM.Email,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName
            };

            var result = await userManager.CreateAsync(user, registerVM.Password);

            if (result.Succeeded)
            {
                logger.LogInformation("User created a new account with password.");

                string code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                await emailService.SendEmailConfirmationAsync(registerVM.Email, callbackUrl);

                // When self registering and login at the same time
                // Need to add/refactor JWT logic if adding
                //await signInManager.SignInAsync(user, isPersistent: false);

                registeredVM.User = mapper.Map<User, UserVM>(user);

                return registeredVM;
            }

            logger.LogWarning("User registration is invalid", user);

            throw new RegistrationFailedException("invalid");
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
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