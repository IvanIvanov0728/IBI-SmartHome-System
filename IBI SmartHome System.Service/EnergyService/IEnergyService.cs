using IBI_SmartHome_System.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.EnergyService
{
	public interface IEnergyService
	{
		Task<EnergyViewModel> GetEnergyDataAsync();
	}
}
