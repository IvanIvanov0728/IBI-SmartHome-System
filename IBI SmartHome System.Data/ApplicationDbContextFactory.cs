using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IBI_SmartHome_System.Data
{
	public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			// This looks one directory up and into the Web project. 
			// Adjust "IBI_SmartHome_System" to match your actual Web project folder name.
			string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "IBI_SmartHome_System");

			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.Exists(path) ? path : Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true) // Make it optional to avoid the crash
				.Build();

			var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

			// Get connection string, or use a hardcoded fallback if config fails to load
			var connectionString = configuration.GetConnectionString("DefaultConnection")
				?? "Server=ibi-smart-home-sys-db-ibi-smart-home-sys-db.c.aivencloud.com;Port=25122;Database=defaultdb;Uid=avnadmin;Pwd=AVNS_Dx3QK_N3tv2lBQvkxGJ;SslMode=Required;CharSet=utf8mb4";

			builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

			return new ApplicationDbContext(builder.Options);
		}
	}
}