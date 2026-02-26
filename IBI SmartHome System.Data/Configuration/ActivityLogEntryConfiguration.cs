using IBI_SmartHome_System.Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Configuration
{
	public class ActivityLogEntryConfiguration
	{
		public void Configure(EntityTypeBuilder<ActivityLogEntry> builder)
		{
			builder.HasData(Seeding.ActivityLogEntrySeeder.Seed());
		}
	}
}
