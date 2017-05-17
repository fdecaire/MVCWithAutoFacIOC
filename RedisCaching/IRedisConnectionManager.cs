using StackExchange.Redis;

namespace RedisCaching
{
	public interface IRedisConnectionManager
	{
		IDatabase RedisServer { get; }
	}
}
