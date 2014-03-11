using System.Web.Http;
using System.Web.Http.Cors;

// We don't need XML comments for this class:
#pragma warning disable 1591

namespace API.App_Start
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			var cors = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(cors);

			config.EnsureInitialized();

			// No config here anymore, we now use attribute routing.
		}
	}
}
