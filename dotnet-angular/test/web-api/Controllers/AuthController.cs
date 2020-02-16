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

        /// <summary>
		/// The constructor of the Auth controller.
		/// </summary>
        public AuthController(
            IConfiguration configuration,
            ILogger<AuthController> logger,
            IMapper mapper,
            AuthBLL bll
        )
        {
            this.configuration = configuration;
            this.logger = logger;
            this.mapper = mapper;
            this.bll = bll;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticatedVM>> Login([FromBody] LoginVM loginVM)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthenticatedVM authenticatedVM = await this.bll.Login(loginVM);

            return Ok(authenticatedVM);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await this.bll.Logout();

            return Ok();
        }

        [HttpGet]
        [Route("me")]
        public async Task<ActionResult<UserVM>> Me()
        {
            User currentUser = await this.bll.Me();

            return Ok(mapper.Map<User, UserVM>(currentUser));
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegisteredVM>> Register([FromBody] RegisterVM registerVM)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RegisteredVM registeredVM = await this.bll.Register(registerVM);

            return Ok(registeredVM);
        }

        [HttpGet]
        [Route("confirm-email")]
        [AllowAnonymous]
        public async Task<ActionResult<EmailConfirmedVM>> ConfirmEmail([FromQuery] string id, [FromQuery] string code)
        {
            // Validation
            if (!ModelState.IsValid || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(code))
            {
                return BadRequest(ModelState);
            }

            EmailConfirmedVM emailConfirmedVM = await this.bll.ConfirmEmail(id, code);

            return Ok(emailConfirmedVM);
        }

        [HttpPost]
        [Route("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordVM forgotPasswordVM)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await this.bll.ForgotPassword(forgotPasswordVM);

            return Ok();
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
            }

            return BadRequest(ModelState);
        }
    }
}
