using System;
using StackExchange.Redis;

namespace RedisCaching
{
	public class RedisConnectionManager : IRedisConnectionManager
	{
		private static string _serverAddress;

		public RedisConnectionManager(string serverAddress)
		{
			_serverAddress = serverAddress;
		}

		private static readonly Lazy<ConfigurationOptions> ConfigOptions = new Lazy<ConfigurationOptions>(() =>
		{
			var configOptions = new ConfigurationOptions();
			configOptions.EndPoints.Add(_serverAddress);
			configOptions.ClientName = "RedisConnection";
			configOptions.ConnectTimeout = 100000;
			configOptions.SyncTimeout = 100000;
			configOptions.AbortOnConnectFail = false;
			return configOptions;
		});

		private static readonly Lazy<ConnectionMultiplexer> Conn = new Lazy<ConnectionMultiplexer>(
			() => ConnectionMultiplexer.Connect(ConfigOptions.Value));

		public IDatabase RedisServer => Conn.Value.GetDatabase(0);
	}
}
