using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Test.API.BLL;
using Test.API.Models;
using Test.API.ViewModels.Identity;

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

        // #-#-#
        // Test
        // #-#-#

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
        public async Task<ActionResult<PasswordResettedVM>> ResetPassword([FromBody] ResetPasswordVM resetPasswordVM)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PasswordResettedVM passwordResettedVM = await this.bll.ResetPassword(resetPasswordVM);

            return Ok(passwordResettedVM);
        }
    }
}
