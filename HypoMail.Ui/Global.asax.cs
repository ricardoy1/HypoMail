namespace MailsManager.Ui
{
    using System.Web.Http;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class MailsWebApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterRoutes();
            RegisterBundles();
            JsonFormatConfig();
        }

        private static void RegisterBundles()
        {
            WebConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void RegisterRoutes()
        {
            GlobalConfiguration.Configure(WebConfig.RegisterWebApiRoutes);
            WebConfig.RegisterMvcRoutes(RouteTable.Routes);
        }

        private static void JsonFormatConfig()
        {
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
