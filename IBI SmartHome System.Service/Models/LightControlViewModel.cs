namespace IBI_SmartHome_System.Service.Models
{
    public class LightControlViewModel
    {
		public int Id { get; set; }
		public int DeviceId { get; set; }
		public string Name { get; set; }
		public bool IsOn { get; set; }
		public int Brightness { get; set; }
		public int RoomId { get; set; }
	}
}
