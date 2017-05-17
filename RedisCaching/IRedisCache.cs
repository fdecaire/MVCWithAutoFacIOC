using System;

namespace RedisCaching
{
	public interface IRedisCache
	{
		T Get<T>(string keyName);
		T Get<T>(string keyName, Func<T> queryFunction);
		T Get<T>(string keyName, int expireTimeInMinutes, Func<T> queryFunction);
		void Expire(string keyName);
		double GetTimeToLive(string keyName);
	}
}
