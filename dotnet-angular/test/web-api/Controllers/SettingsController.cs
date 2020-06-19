using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJM.API.BLL;
using RJM.API.Models;
using RJM.API.ViewModels;

namespace RJM.API.Controllers
{
	/// <summary>
	/// The Settings controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<SettingsController> logger;
        private readonly IMapper mapper;
        private readonly SettingBLL bll;

		/// <summary>
		/// The constructor of the Settings controller.
		/// </summary>
        public SettingsController(
            ILogger<SettingsController> logger,
			IMapper mapper,
            SettingBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/settings
		/// <summary>
		/// Retrieves all settings.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SettingVM>>> GetSettings()
        {
            IEnumerable<Setting> settings = await this.bll.GetAllSettingsAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<Setting>, List<SettingVM>>(settings));
        }

        // GET: api/settings/{id}
		/// <summary>
		/// Retrieves a specific setting.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<SettingVM>> GetSetting([FromRoute] Guid id)
        {
            Setting setting = await this.bll.GetSettingByIdAsync(id);
            if (setting == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<Setting, SettingVM>(setting));
        }

        // POST: api/settings
		/// <summary>
		/// Creates a new setting.
		/// </summary>
		/// <param name="settingVM"></param>
        [HttpPost]
        public async Task<ActionResult<SettingVM>> CreateSetting([FromBody] SettingVM settingVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Setting setting = this.mapper.Map<SettingVM, Setting>(settingVM);

            setting = await this.bll.CreateSettingAsync(setting);

			// Mapping
            return CreatedAtAction(
				"GetSetting",
				new { id = setting.Id },
				this.mapper.Map<Setting, SettingVM>(setting)
			);
        }

		// PUT: api/settings/{id}
		/// <summary>
		/// Updates a specific setting.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="settingVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<SettingVM>> UpdateSetting([FromRoute] Guid id, [FromBody] SettingVM settingVM)
        {
			// Validation
            if (!ModelState.IsValid || id != settingVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            Setting setting = this.mapper.Map<SettingVM, Setting>(settingVM);

            setting = await this.bll.UpdateSettingAsync(setting);

			// Mapping
			return Ok(this.mapper.Map<Setting, SettingVM>(setting));
        }

        // DELETE: api/settings/{id}
		/// <summary>
		/// Deletes a specific setting.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<SettingVM>> DeleteSetting([FromRoute] Guid id)
        {
            // Retrieve existing setting
            Setting setting = await this.bll.GetSettingByIdAsync(id);
            if (setting == null)
            {
                return NotFound();
            }

            await this.bll.DeleteSettingAsync(setting);

            // Mapping
            return Ok(this.mapper.Map<Setting, SettingVM>(setting));
        }
    }
}
