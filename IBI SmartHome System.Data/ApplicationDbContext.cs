using IBI_SmartHome_System.Data.Configuration;
using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Data.Seeding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace IBI_SmartHome_System.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

		#region Tables
		/*
            public DbSet<Room> Rooms { get; set; }
            public DbSet<Device> Devices { get; set; }
            public DbSet<MqttMessage> MqttMessages { get; set; }
            public DbSet<Lamp> Lamps { get; set; }
            public DbSet<Temperature> Temperature { get; set; }
         */
		#endregion


		#region Seed Data And Fluent API Configurations

		//                    Relationships
		//
		// Room has many Devices 1 -- M (A room has many devices) 👍
		// Device has one Lamp 1 -- 1 (Each lamp belongs to one device) 👍
		// Device has Many Temperature 1 -- 1 (Each room can have max of one termostat)👍
		// Device has many MqttMessages 1 -- M (Each device can send/receive many MQTT messages) 👍
		protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

			base.OnModelCreating(modelBuilder);

				#region Flush API Configurations
			modelBuilder.Entity<Device>()
					.HasOne(d => d.Room)
					.WithMany(r => r.Devices)
					.HasForeignKey(d => d.RoomId);

				modelBuilder.Entity<Lamp>()
					.HasOne(l => l.Device)
					.WithOne(d => d.Lamp)
					.HasForeignKey<Lamp>(l => l.DeviceId);

				modelBuilder.Entity<Temperature>()
					.HasOne(t => t.Device)
					.WithOne(d => d.Temperature)
					.HasForeignKey<Temperature>(t => t.DeviceId);
				
				modelBuilder.Entity<MqttMessage>()
					.HasOne(mm => mm.Device)
					.WithMany(d => d.MqttMessages)
					.HasForeignKey(mm => mm.DeviceId);
			#endregion

				#region Seed Data
				modelBuilder.ApplyConfiguration(new RoomsConfiguration());
				modelBuilder.ApplyConfiguration(new DevicesConfiguration());
				modelBuilder.ApplyConfiguration(new LampsConfiguration());
				modelBuilder.ApplyConfiguration(new MotionSensorsConfiguration());
				modelBuilder.ApplyConfiguration(new TemperatureConfiguration());
			#endregion

			}
		#endregion
	}
}
