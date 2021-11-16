using Microsoft.Web.Http;
using Microsoft.Web.Http.Routing;
using Microsoft.Web.Http.Versioning;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Sistema.Web.API.SwaggerExt;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;

namespace Sistema.Web.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // AppSettings
            AppSettings.Register();

            // Config: Enabling Bearer Authentication
            if (AppSettings.EnableTokenAuthorization == true)
            {
                // Add Authorize Attribute Global Filter
                config.Filters.Add(new AuthorizeAttribute());

                // Add AuthHandler
                config.MessageHandlers.Add(new AuthHandler());

                // Web API configuration and services
                var cors = new EnableCorsAttribute("*", "*", "*");
                config.EnableCors(cors);
            }

            // ApiVersion
            config.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                //o.DefaultApiVersion = new ApiVersion(2, 0);
                //o.AssumeDefaultVersionWhenUnspecified = true;
                //o.ApiVersionReader = new UrlSegmentApiVersionReader();
                //o.ApiVersionSelector = new CurrentImplementationApiVersionSelector(o);
            });

            var constraintResolver = new DefaultInlineConstraintResolver() { ConstraintMap = { ["apiVersion"] = typeof(ApiVersionRouteConstraint) } };

            // Web API routes
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            // Formatters
            var formatters = config.Formatters;
            var xmlFormatter = formatters.XmlFormatter;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;

            // Config: Enable Json Response
            if (AppSettings.EnableOnlyJsonResponseType == true)
            {
                // Remove XML formatter
                formatters.Remove(xmlFormatter);

                // Resolve Default Type Response
                formatters.Add(new BrowserJsonFormatter());
            }

            // Resolve Json settings
            settings =
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Converters = new[] { new StringEnumConverter() }
                };

            // Resolve Json Serializable attribute
            settings.ContractResolver =
                new DefaultContractResolver
                {
                    IgnoreSerializableAttribute = true
                };


            // SwaggerConfig
            SwaggerConfig.Register();

        }
    }


}