using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Routing;
using SimpleInjector.Integration.WebApi;

namespace CompanyStores.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			// WebApi
			GlobalConfiguration.Configure(WebApiConfig.Register);

			var connectionString =
				"Data Source=DESKTOP-58GBQIE;Initial Catalog=CompanyStores;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			// setup DI
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
			container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
			container.RegisterMvcIntegratedFilterProvider();

			// inject stuff to make business logic work
			DI.DependencyInjector.RegisterBLL(container);
			// TODO fix this
			DI.DependencyInjector.RegisterDALEntityFramework6(container, connectionString);

			container.Verify();

			// for WebApi
			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

			DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
		}
	}
}
