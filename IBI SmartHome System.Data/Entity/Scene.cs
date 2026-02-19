using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class Scene
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public ICollection<SceneAction> SceneActions { get; set; } = new List<SceneAction>();
	}
}
