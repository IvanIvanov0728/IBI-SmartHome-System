using IBI_SmartHome_System.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.LightingService
{
	public interface ILightingService
	{
		LightingViewModel GetLightingViewModel();
	}
}
