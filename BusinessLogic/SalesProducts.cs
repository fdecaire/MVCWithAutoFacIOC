using System.Collections.Generic;
using System.Linq;
using DAC;
using RedisCaching;

namespace BusinessLogic
{
    public class SalesProducts : ISalesProducts
    {
	    private readonly IDatabaseContext _db;
	    private readonly IRedisCache _cache;

	    public SalesProducts(IDatabaseContext db, IRedisCache cache)
	    {
			_db = db;
		    _cache = cache;
		}

		public List<string> Top10ProductNames()
	    {
			return _cache.Get("Top10ProductList", 60, () => (from p in _db.Products select p.Name).Take(10).ToList());
		}
	}
}
