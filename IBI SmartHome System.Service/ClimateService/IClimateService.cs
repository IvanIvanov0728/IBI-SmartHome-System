using IBI_SmartHome_System.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.ClimateService
{
	public interface IClimateService
	{
		Task<ClimateViewModel> GetClimateViewModelAsync();
		Task UpdateTargetTemperature(int targetTemperature);
		Task<IEnumerable<ClimateScheduleViewModel>> GetScheduleAsync();
		Task<ClimateScheduleViewModel> AddScheduleEntryAsync(ClimateScheduleViewModel newEntry);
		Task<bool> UpdateScheduleEntryAsync(int id, ClimateScheduleViewModel updatedEntry);
		Task<bool> DeleteScheduleEntryAsync(int id);
	}
}
