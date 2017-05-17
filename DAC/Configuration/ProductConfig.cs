using System.Data.Entity;
using DAC.Domain;

namespace DAC.Configuration
{
	public static class ProductConfig
	{
		public static void ProductMapping(this DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.ToTable("Product");

			modelBuilder.Entity<Product>()
				.HasKey(e => e.Id);
			
			modelBuilder.Entity<Product>()
				.Property(e => e.Id)
				.HasColumnType("int")
				.HasColumnName("Id")
				.IsRequired();
			
			modelBuilder.Entity<Product>()
				.Property(e => e.Name)
				.HasColumnType("varchar")
				.HasColumnName("Name")
				.IsOptional();
			
			modelBuilder.Entity<Product>()
				.Property(e => e.Price)
				.HasColumnType("money");
			
			modelBuilder.Entity<Product>()
				.HasRequired(d => d.StoreNavigation)
				.WithMany(p => p.Product)
				.HasForeignKey(d => d.Store)
				.WillCascadeOnDelete(true);
			
		}
	}
}
