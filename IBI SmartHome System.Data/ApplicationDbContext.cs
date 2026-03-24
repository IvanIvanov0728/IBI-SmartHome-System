using IBI_SmartHome_System.Data.Configuration;
using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Data.Seeding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace IBI_SmartHome_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

		#region Tables

			public DbSet<Room> Room { get; set; }
			public DbSet<Device> Device { get; set; }
			public DbSet<MqttMessage> MqttMessages { get; set; }
			public DbSet<Lamp> Lamps { get; set; }
			public DbSet<Temperature> Temperature { get; set; }
			public DbSet<MotionSensor> MotionSensor { get; set; }
			public DbSet<Scene> Scenes { get; set; }
			public DbSet<SceneAction> SceneActions { get; set; }
			public DbSet<ClimateSchedule> ClimateSchedules { get; set; }
			public DbSet<ActivityLogEntry> ActivityLogEntries { get; set; }
			public DbSet<Camera> Cameras { get; set; }
			public DbSet<House> Houses { get; set; }
			public DbSet<AutomationRule> AutomationRules { get; set; }

		#endregion


		#region Seed Data And Fluent API Configurations

		protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

			base.OnModelCreating(modelBuilder);

			#region Flush API Configurations
			// 1. House to User
			modelBuilder.Entity<House>()
				.HasOne(h => h.User)
				.WithMany()
				.HasForeignKey(h => h.UserId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			// 2. House to Room
			modelBuilder.Entity<House>()
				.HasMany(h => h.Rooms)
				.WithOne(r => r.House)
				.HasForeignKey(r => r.HouseId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			// 3. Device Relationships
			modelBuilder.Entity<Device>()
				.HasOne(d => d.House)
				.WithMany()
				.HasForeignKey(d => d.HouseId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			modelBuilder.Entity<Device>()
				.HasOne(d => d.Room)
				.WithMany(r => r.Devices)
				.HasForeignKey(d => d.RoomId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			// 4. Scene and SceneAction
			modelBuilder.Entity<SceneAction>()
				.HasOne(sa => sa.Scene)
				.WithMany(s => s.SceneActions)
				.HasForeignKey(sa => sa.SceneId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<SceneAction>()
				.HasOne(sa => sa.Device)
				.WithMany()
				.HasForeignKey(sa => sa.DeviceId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<AutomationRule>()
				.HasOne(ar => ar.TriggerDevice) // Assuming this is the navigation property name
				.WithMany()
				.HasForeignKey(ar => ar.TriggerDeviceId)
				.OnDelete(DeleteBehavior.Restrict); // Change from Cascade to Restrict

			modelBuilder.Entity<AutomationRule>()
				.HasOne(ar => ar.ActionDevice) // Assuming this is the navigation property name
				.WithMany()
				.HasForeignKey(ar => ar.ActionDeviceId)
				.OnDelete(DeleteBehavior.Restrict);

			// 5. House to Other Entities
			modelBuilder.Entity<House>().HasMany(h => h.Scenes).WithOne(s => s.House).HasForeignKey(s => s.HouseId).OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<House>().HasMany(h => h.ClimateSchedules).WithOne(cs => cs.House).HasForeignKey(cs => cs.HouseId).OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<House>().HasMany(h => h.ActivityLogEntries).WithOne(ale => ale.House).HasForeignKey(ale => ale.HouseId).OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<House>().HasMany(h => h.Cameras).WithOne(c => c.House).HasForeignKey(c => c.HouseId).OnDelete(DeleteBehavior.Cascade);

			// 6. Component Relationships
			modelBuilder.Entity<Lamp>().HasOne(l => l.Device).WithOne(d => d.Lamp).HasForeignKey<Lamp>(l => l.DeviceId).OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<Temperature>().HasOne(t => t.Device).WithOne(d => d.Temperature).HasForeignKey<Temperature>(t => t.DeviceId).OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<MotionSensor>().HasOne(ms => ms.Device).WithOne(d => d.MotionSensor).HasForeignKey<MotionSensor>(ms => ms.DeviceId).OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<MqttMessage>().HasOne(mm => mm.Device).WithMany(d => d.MqttMessages).HasForeignKey(mm => mm.DeviceId).OnDelete(DeleteBehavior.Cascade);
			#endregion

			#region Seed Data

				modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
				{
					Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
					UserName = "admin@smarthome.com",
					NormalizedUserName = "ADMIN@SMARTHOME.COM",
					Email = "admin@smarthome.com",
					NormalizedEmail = "ADMIN@SMARTHOME.COM",
					EmailConfirmed = true,
					UserRole = "Admin"
				});

				modelBuilder.ApplyConfiguration(new HousesConfiguration());
				modelBuilder.ApplyConfiguration(new RoomsConfiguration());
				modelBuilder.ApplyConfiguration(new DevicesConfiguration());
				modelBuilder.ApplyConfiguration(new LampsConfiguration());
				modelBuilder.ApplyConfiguration(new MotionSensorsConfiguration());
				modelBuilder.ApplyConfiguration(new TemperatureConfiguration());
				modelBuilder.ApplyConfiguration(new ScenesConfiguration());
				modelBuilder.ApplyConfiguration(new SceneActionsConfiguration());
				modelBuilder.ApplyConfiguration(new ClimateScheduleConfiguration());
				modelBuilder.ApplyConfiguration(new ActivityLogEntryConfiguration());
				modelBuilder.ApplyConfiguration(new CamerasConfiguration());
			#endregion

		}
		#endregion
	}
}
