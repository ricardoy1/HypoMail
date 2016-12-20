namespace MailsManager.Ui
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    /// <summary>
    /// Application Configuration
    /// </summary>
    public static class WebConfig
    {
        /// <summary>
        /// Registers Web API.
        /// </summary>
        /// <param name="config">Http configuration.</param>
        public static void RegisterWebApiRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "mailsManagerApi",
                "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });
        }

        /// <summary>
        /// Registers MVC route.
        /// </summary>
        /// <param name="routes">Route collection.</param>
        public static void RegisterMvcRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" });
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Styles")
                .Include("~/Content/bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/Scripts")
                .Include("~/Scripts/angular.min.js")
                .Include("~/Scripts/angular-route.js")
                .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.min.js")
                .Include("~/app/mailsManagerApp.js")
                .Include("~/app/Mail/Controllers/mailCtrl.js")
                .Include("~/app/Mail/Services/mailService.js"));
        }
    }
}
