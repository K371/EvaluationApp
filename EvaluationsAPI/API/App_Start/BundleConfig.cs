using System.Web;
using System.Web.Optimization;

// We don't need XML comments for this class:
#pragma warning disable 1591

namespace API.App_Start
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

			//Bootstrap
			bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
			bundles.Add(new StyleBundle("~/Content/bootstrap-responsive").Include("~/Content/bootstrap-responsive.css"));
			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

			//Dot dot dot
			bundles.Add(new ScriptBundle("~/bundles/dotdotdot").Include("~/Scripts/jquery.dotdotdot.js"));

			//custom js
			bundles.Add(new ScriptBundle("~/bundles/site").Include("~/Scripts/site.js"));
		}
	}
}