namespace IBI_SmartHome_System.Models
{
	public class AddActivityLogRequest
	{
		public string EventDescription { get; set; }
		public string Type { get; set; }
		public int? DeviceId { get; set; }
	}
}
