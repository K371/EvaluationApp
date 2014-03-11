using System.Web.Mvc;
using System.Web.Routing;

// We don't need XML comments for this class:
#pragma warning disable 1591

namespace API.App_Start
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
/*
			// Basic REST url 
			routes.MapRoute(
				name: "RESTGroup",
				url: "docs/{method}/{groupname}/{id}/{param}",
				defaults: new
				{
					controller = "Documentation",
					action = "RESTGroup"
				}
			);
			routes.MapRoute(
				name: "RESTGroupNoParam",
				url: "docs/{method}/{groupname}/{id}",
				defaults: new
				{
					controller = "Documentation",
					action = "RESTGroupNoParam",
				}
			);
			routes.MapRoute(
			   name: "RESTGroupNoIDNoParam",
			   url: "docs/{method}/{groupname}",
			   defaults: new
			   {
				   controller = "Documentation",
				   action = "RESTGroupNoIDNoParam",
			   }
		   );

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
				);
 * */
		}
	}
}
