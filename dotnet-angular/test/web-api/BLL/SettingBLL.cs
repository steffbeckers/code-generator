using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.BLL
{
	/// <summary>
	/// The business logic layer for Settings.
	/// </summary>
    public class SettingBLL
    {
        private readonly SettingRepository settingRepository;

		/// <summary>
		/// The constructor of the Setting business logic layer.
		/// </summary>
        public SettingBLL(
			SettingRepository settingRepository
		)
        {
            this.settingRepository = settingRepository;
        }

		/// <summary>
		/// Retrieves all settings.
		/// </summary>
		public async Task<IEnumerable<Setting>> GetAllSettingsAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.settingRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one setting by Id.
		/// </summary>
		public async Task<Setting> GetSettingByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.settingRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new setting record.
		/// </summary>
        public async Task<Setting> CreateSettingAsync(Setting setting)
        {
            // Validation
            if (setting == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(setting.Key))
                setting.Key = setting.Key.Trim();
            if (!string.IsNullOrEmpty(setting.Value))
                setting.Value = setting.Value.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			setting = await this.settingRepository.InsertAsync(setting);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return setting;
        }

		/// <summary>
		/// Updates an existing setting record by Id.
		/// </summary>
        public async Task<Setting> UpdateSettingAsync(Setting settingUpdate)
        {
            // Validation
            if (settingUpdate == null) { return null; }

            // Retrieve existing
            Setting setting = await this.settingRepository.GetByIdAsync(settingUpdate.Id);
            if (setting == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(settingUpdate.Key))
                settingUpdate.Key = settingUpdate.Key.Trim();
            if (!string.IsNullOrEmpty(settingUpdate.Value))
                settingUpdate.Value = settingUpdate.Value.Trim();

            // Mapping
            setting.Key = settingUpdate.Key;
            setting.Value = settingUpdate.Value;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			setting = await this.settingRepository.UpdateAsync(setting);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return setting;
        }

		/// <summary>
		/// Deletes an existing setting record by Id.
		/// </summary>
        public async Task<Setting> DeleteSettingByIdAsync(Guid settingId)
        {
            Setting setting = await this.settingRepository.GetByIdAsync(settingId);

            return await this.DeleteSettingAsync(setting);
        }

		/// <summary>
		/// Deletes an existing setting record.
		/// </summary>
        public async Task<Setting> DeleteSettingAsync(Setting setting)
        {
            // Validation
            if (setting == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.settingRepository.DeleteAsync(setting);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return setting;
        }
    }
}
