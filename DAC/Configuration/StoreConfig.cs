using System.Data.Entity;
using DAC.Domain;

namespace DAC.Configuration
{
	public static class StoreConfig
	{
		public static void StoreMapping(this DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Store>()
				.ToTable("Store");

			modelBuilder.Entity<Store>()
				.HasKey(e => e.Id);

			modelBuilder.Entity<Store>()
				.Property(e => e.Id)
				.HasColumnType("int")
				.HasColumnName("Id")
				.IsRequired();

			modelBuilder.Entity<Store>()
				.Property(e => e.Address)
				.HasColumnType("varchar")
				.IsOptional();

			modelBuilder.Entity<Store>()
				.Property(e => e.Name)
				.HasColumnType("varchar")
				.IsOptional();

			modelBuilder.Entity<Store>()
				.Property(e => e.State)
				.HasColumnType("varchar")
				.IsOptional();

			modelBuilder.Entity<Store>()
				.Property(e => e.Zip)
				.HasColumnType("varchar")
				.IsOptional();
		}
	}
}
