using IBI_SmartHome_System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Seeding
{
	public class TemperatureConfiguration : IEntityTypeConfiguration<Temperature>
	{
		public void Configure(EntityTypeBuilder<Temperature> builder)
		{
			builder.HasData(TemperatureSeeder.Seed());
		}
	}
}
