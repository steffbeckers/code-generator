using AutoMapper;
using Test.API.Models;
using Test.API.Services;
using Test.API.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Test.API.BLL;

namespace Test.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly AuthBLL bll;

        // TODO: Move to bll
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailService emailService;

        /// <summary>
		/// The constructor of the Auth controller.
		/// </summary>
        public AuthController(
            IConfiguration configuration,
            ILogger<AuthController> logger,
            IMapper mapper,
            AuthBLL bll)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.mapper = mapper;
            this.bll = bll;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResultVM>> Login([FromBody] LoginVM loginVM)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoginResultVM loginResultVM = await this.bll.Login(loginVM);

            // Error
            if (!string.IsNullOrEmpty(loginResultVM.Error)) {
                return BadRequest(loginResultVM);
            }

            return Ok(loginResultVM);
        }

        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> Me()
        {
            User currentUser = await userManager.GetUserAsync(User);

            // Retrieve roles of user
            currentUser.Roles = (List<string>)await userManager.GetRolesAsync(currentUser);

            return Ok(mapper.Map<User, UserVM>(currentUser));
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username, Email = model.Email };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id.ToString().ToUpper(), code, Request.Scheme);
                    await emailService.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    // When self registering and login at the same time
                    // Need to add/refactor JWT logic if adding
                    //await signInManager.SignInAsync(user, isPersistent: false);

                    return Ok();
                }

                AddErrors(result);
            }

            // If we got this far, something failed
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Ok();
        }

        [HttpGet]
        [Route("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Ok();
            }

            AddErrors(result);

            // If we got this far, something failed
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    //// Don't reveal that the user does not exist
                    //return Ok();

                    // For CRM purposes
                    return NotFound("email-not-found");
                }

                // Check if email is confirmed, if required in Startup settings
                // In startup: options.SignIn.RequireConfirmedEmail = true;
                if (!(await userManager.IsEmailConfirmedAsync(user)))
                {
                    //// Don't reveal that the user does not exist
                    //return Ok();

                    // OR

                    return NotFound("email-not-confirmed");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713

                var code = await userManager.GeneratePasswordResetTokenAsync(user);

                //var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                var callbackUrl = configuration.GetSection("EmailSettings").GetValue<string>("PasswordResetURL");
                callbackUrl = callbackUrl.Replace("{{userId}}", user.Id.ToString().ToLower());
                callbackUrl = callbackUrl.Replace("{{userEmail}}", user.Email.ToString().ToLower());
                callbackUrl = callbackUrl.Replace("{{code}}", Uri.EscapeDataString(code));

                await emailService.SendEmailAsync(model.Email, "Reset Password",
                   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

                return Ok();
            }

            // If we got this far, something failed
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    //return BadRequest();

                    // OR

                    // For CRM purposes
                    return NotFound("user-not-found");
                }
                if (user.Email != model.Email)
                {
                    //return BadRequest();

                    // OR

                    // For CRM purposes
                    return BadRequest("email-does-not-match");
                }

                var result = await userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    // TODO: Maybe log the user in automatically? Need to add/refactor JWT logic if adding
                    //await signInManager.SignInAsync(user, isPersistent: false);

                    return Ok();
                }

                AddErrors(result);
            }

            return BadRequest(ModelState);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}
