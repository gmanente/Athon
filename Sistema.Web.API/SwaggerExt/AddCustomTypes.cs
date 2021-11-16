using Swashbuckle.Swagger;
using System.Web.Http.Description;

namespace Sistema.Web.API.SwaggerExt
{
    public class AddCustomTypes : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            // Type Multipart
            if (operation.consumes.Contains("application/x-www-form-urlencoded"))
                operation.consumes.Remove("application/x-www-form-urlencoded");

            // Type HTML
            if (operation.consumes.Contains("text/html"))
                operation.consumes.Remove("text/html");
            if (operation.produces.Contains("text/html"))
                operation.produces.Remove("text/html");

            // Type XML
            if (AppSettings.EnableOnlyJsonResponseType == false)
            {
                if (operation.produces.Contains("text/xml"))
                    operation.produces.Remove("text/xml");

                if (!operation.produces.Contains("application/xml"))
                    operation.produces.Add("application/xml");
            }

            // Type JSON
            //if (operation.consumes.Contains("text/json"))
            //    operation.consumes.Remove("text/json");
            //if (operation.produces.Contains("text/json"))
            //    operation.produces.Remove("text/json");

            if (!operation.produces.Contains("application/json"))
                operation.produces.Add("application/json");
        }
    }
}