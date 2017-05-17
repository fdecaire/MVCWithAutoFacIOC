using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using BusinessLogic;
using DAC;
using RedisCaching;

namespace MVCwithAutoFacIOC
{
	public class AutofacBootstrapper
	{
		public static void Run()
		{
			var builder = new ContainerBuilder();

			// Register your MVC controllers. (MvcApplication is the name of
			// the class in Global.asax.)
			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			// OPTIONAL: Register model binders that require DI.
			builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
			builder.RegisterModelBinderProvider();

			// OPTIONAL: Register web abstractions like HttpContextBase.
			builder.RegisterModule<AutofacWebTypesModule>();

			// OPTIONAL: Enable property injection in view pages.
			builder.RegisterSource(new ViewRegistrationSource());

			// OPTIONAL: Enable property injection into action filters.
			builder.RegisterFilterProvider();

			builder.Register(c => new DatabaseContext("Server=DECAIREPC;Initial Catalog=DemoData;Integrated Security=True"))
				.As<IDatabaseContext>()
				.PropertiesAutowired()
				.InstancePerLifetimeScope();

			builder.Register(c => new RedisConnectionManager("127.0.0.1"))
				.As<IRedisConnectionManager>()
				.PropertiesAutowired()
				.SingleInstance();

			builder.Register(c => new RedisCache(c.Resolve<IRedisConnectionManager>()))
				.As<IRedisCache>()
				.PropertiesAutowired()
				.SingleInstance();

			builder.Register(c => new SalesProducts(c.Resolve<IDatabaseContext>(), c.Resolve<IRedisCache>()))
				.As<ISalesProducts>()
				.PropertiesAutowired()
				.InstancePerLifetimeScope();

			// Set the dependency resolver to be Autofac.
			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}