using IBI_SmartHome_System.UITests.Models;

namespace IBI_SmartHome_System.UITests.Factories
{
	public static class UserFactory
	{
		public static UserModel GetValidUser()
		{
			return new UserModel
			{
				Email = "admin@smarthome.com",
				Password = "Password123!"
			};
		}
	}
}