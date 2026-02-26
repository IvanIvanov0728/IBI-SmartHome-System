using IBI_SmartHome_System.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.SettingsService
{
	public interface ISettingsService
	{
		Task<UserProfileViewModel> GetUserProfileAsync();
		Task<bool> UpdateUserProfileAsync(UserProfileViewModel model);
		Task<UserSettingsViewModel> GetUserSettingsAsync();
		Task<bool> UpdateUserSettingsAsync(UserSettingsViewModel model);
	}
}
