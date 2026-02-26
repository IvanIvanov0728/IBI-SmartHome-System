using IBI_SmartHome_System.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.SecurityService
{
	public interface ISecurityService
	{
		Task<SecurityViewModel> GetSecurityOverviewAsync();
		Task<bool> UpdateEntryPointLockStatus(int deviceId, bool isLocked);
		Task AddActivityLogEntryAsync(string eventDescription, string type, int? deviceId = null);
	}
}
