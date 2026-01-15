using IBI_SmartHome_System.Service.Models;

namespace IBI_SmartHome_System.Service.DashboardService
{
	public interface IDashboardService
	{
		Task<DashboardViewModel> GetDashboardViewModelAsync();
		ThermostatViewModel GetThermostatViewModel();
	}
}
