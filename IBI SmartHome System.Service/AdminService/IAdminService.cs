using IBI_SmartHome_System.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.AdminService
{
	public interface IAdminService
	{
		Task<List<UserSearchResultViewModel>> SearchUsersAsync(string query);
		Task<int> CreateHouseAsync(CreateHouseViewModel model);
		Task<int> AddRoomToHouseAsync(CreateRoomViewModel model);
		Task<int> AddDeviceToRoomAsync(CreateDeviceViewModel model);
		Task<List<HouseHierarchyViewModel>> GetHousesWithHierarchyAsync();
		Task<List<AdminActivityLogViewModel>> GetGlobalActivityLogsAsync();
		Task LogActionAsync(int houseId, string eventDescription, string type, int? deviceId = null);
		Task<AdminAnalyticsViewModel> GetGlobalAnalyticsAsync();
		Task<List<AutomationRuleViewModel>> GetAutomationRulesAsync();
		Task<int> CreateAutomationRuleAsync(CreateAutomationRuleViewModel model);
		Task DeleteAutomationRuleAsync(int id);
	}
}
