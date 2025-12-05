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
	public class LampsConfiguration : IEntityTypeConfiguration<Lamp>
	{
		public void Configure(EntityTypeBuilder<Lamp> builder)
		{
			builder.HasData(LampSeeder.Seed());
		}
	}
}
