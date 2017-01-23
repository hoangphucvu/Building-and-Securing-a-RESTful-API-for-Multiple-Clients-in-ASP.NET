using System.Data.Entity;
using System.Web;
using System.Web.Http;
using JWT.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JWT
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var formatter = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatter.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer(new Initializer());
        }
    }
}