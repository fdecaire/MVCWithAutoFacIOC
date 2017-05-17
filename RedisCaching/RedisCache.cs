using System;
using Newtonsoft.Json;

namespace RedisCaching
{
	public class RedisCache : IRedisCache
	{
		private readonly IRedisConnectionManager _cacheConnection;

		public RedisCache(IRedisConnectionManager cacheConnection)
		{
			_cacheConnection = cacheConnection;
		}

		public T Get<T>(string keyName)
		{
			try
			{
				string data = null;
				try
				{
					data = _cacheConnection.RedisServer.StringGet(keyName);
				}
				catch (Exception ex)
				{
					//TODO: need logging here
				}

				return data == null ? default(T) : JsonConvert.DeserializeObject<T>(data);
			}
			catch (Exception ex)
			{
				//TODO: should log this
				return default(T);
			}
		}

		public T Get<T>(string keyName, Func<T> queryFunction)
		{
			return Get<T>(keyName, 60, queryFunction);
		}

		public T Get<T>(string keyName, int expireTimeInMinutes, Func<T> queryFunction)
		{
			try
			{
				string data = null;

				try
				{
					data = _cacheConnection.RedisServer.StringGet(keyName);
				}
				catch (Exception ex)
				{
					// TODO: logging here
				}

				if (data == null)
				{
					var result = queryFunction();

					if (result != null)
					{
						try
						{
							_cacheConnection.RedisServer.StringSet(keyName, JsonConvert.SerializeObject(result), new TimeSpan(0, expireTimeInMinutes, 0));
						}
						catch (Exception ex)
						{
							// TODO: logging here
						}
					}

					return result;
				}

				return JsonConvert.DeserializeObject<T>(data);
			}
			catch (Exception ex)
			{
				// TODO: logging here
				return default(T);
			}
		}

		public void Expire(string keyName)
		{
			try
			{
				_cacheConnection.RedisServer.KeyDelete(keyName);
			}
			catch (Exception ex)
			{
				// TODO: logging here
			}
		}

		public double GetTimeToLive(string keyName)
		{
			try
			{
				var result = _cacheConnection.RedisServer.KeyTimeToLive(keyName);
				if (result == null)
				{
					return -1; // key has expired or does not exist
				}
				else
				{
					var span = result ?? TimeSpan.Zero;
					return span.TotalMinutes;
				}
			}
			catch (Exception ex)
			{
				// TODO: logging here
				return -1;
			}
		}
	}
}
