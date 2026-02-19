using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Configuration
{
	public class ScenesConfiguration : IEntityTypeConfiguration<Scene>
	{
		public void Configure(EntityTypeBuilder<Scene> builder)
		{
			builder.HasData(SceneSeeder.Seed());
		}
	}
}
