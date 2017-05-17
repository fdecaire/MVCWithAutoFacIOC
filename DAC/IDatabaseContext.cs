using System.Data.Entity;
using DAC.Domain;

namespace DAC
{
	public interface IDatabaseContext
	{
		DbSet<Store> Stores { get; set; }
		DbSet<Product> Products { get; set; }
	}
}
