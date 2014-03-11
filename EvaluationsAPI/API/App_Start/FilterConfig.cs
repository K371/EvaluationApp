using System.Web.Mvc;

// We don't need XML comments for this class:
#pragma warning disable 1591

namespace API.App_Start
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}