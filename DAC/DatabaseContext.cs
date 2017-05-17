using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DAC.Configuration;
using DAC.Domain;

namespace DAC
{
	public class DatabaseContext : DbContext, IDatabaseContext
	{
		public DatabaseContext(string connectionString)
			: base(connectionString)
		{
			Database.SetInitializer<DatabaseContext>(null);
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			modelBuilder.StoreMapping();
			modelBuilder.ProductMapping();
		}

		public virtual DbSet<Store> Stores { get; set; }
		public virtual DbSet<Product> Products { get; set; }
	}
}
