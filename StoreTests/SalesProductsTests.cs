using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessLogic;
using DAC;
using DAC.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RedisCaching;

namespace StoreTests
{
	[TestClass]
	public class SalesProductsTests
	{
		[TestMethod]
		public void Top10ProductsTest()
		{
			var cache = new RedisCache(new FakeRedisConnection());

			var database = new Mock<IDatabaseContext>();

			var data = new List<Product>
			{
				new Product {Name = "Soda", Price = 3.99m, Store = 0},
				new Product {Name = "Bananas", Price = 2.30m, Store = 0},
				new Product {Name = "Potato Chips", Price = 3.99m, Store = 0},
				new Product {Name = "Bread", Price = 3.99m, Store = 0},
				new Product {Name = "Onions", Price = 1.99m, Store = 0},
				new Product {Name = "Apples", Price = 1.99m, Store = 0},
				new Product {Name = "Olives", Price = 1.99m, Store = 0},
				new Product {Name = "Crackers", Price = 1.99m, Store = 0},
				new Product {Name = "Peanut Butter", Price = 1.99m, Store = 0},
				new Product {Name = "Jelly", Price = 1.99m, Store = 0},
				new Product {Name = "Milk", Price = 1.99m, Store = 0},
				new Product {Name = "Cheese", Price = 1.99m, Store = 0},
				new Product {Name = "Sausage", Price = 1.99m, Store = 0},
				new Product {Name = "Hamburger", Price = 1.99m, Store = 0},
				new Product {Name = "Sardines", Price = 1.99m, Store = 0},
				new Product {Name = "Nuts", Price = 1.97m, Store = 0}

			}.AsQueryable();

			var productSet = new Mock<DbSet<Product>>();
			productSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
			productSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
			productSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);

			database.Setup(c => c.Products).Returns(productSet.Object);

			var salesProduct = new SalesProducts(database.Object, cache);

			var result = salesProduct.Top10ProductNames();

			Assert.AreEqual(10, result.ToList().Count);
		}
	}
}
